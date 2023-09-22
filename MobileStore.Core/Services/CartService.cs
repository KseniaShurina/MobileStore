using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using MobileStore.Common.Abstractions.Services;
using MobileStore.Common.Models;
using MobileStore.Core.Abstractions.Services;
using MobileStore.Core.Extensions.Entities;
using MobileStore.Core.Models;
using MobileStore.Infrastructure.Abstractions.Contexts;
using MobileStore.Infrastructure.Entities;

namespace MobileStore.Core.Services
{
    internal class CartService : ICartService
    {
        private readonly IDefaultContext _context;
        private readonly IReadIdentityService _identityService;

        public CartService(IDefaultContext context, IReadIdentityService identityService)
        {
            _context = context;
            _identityService = identityService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IQueryable<CartItem> GetBaseQuery()
        {
            return _context.CartItems
                .AsNoTracking()
                .Include(i => i.Product);
        }

        /// <summary>
        /// Got cart item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private Task<CartItem?> Get(Guid id)
        {
            return GetBaseQuery()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<ProductTypeModel>> GetProductTypes()
        {
            var entities = await _context.ProductTypes
                .AsNoTracking().ToListAsync();
            return entities
                .Select(MapFromEntity)
                .ToList();
        }

        public async Task<List<CartItemModel>> GetCartItems()
        {
            var userId = _identityService.UserId!.Value;
            var entity = await GetBaseQuery()
                .Where(i => i.UserId == userId)
                .ToListAsync();
            
            //var mapper = new MapperConfiguration(x => x.CreateMap(CartItem, CartItemDto)).CreateMapper();
            //TODO 
            //if(entity == null) ?? throw new ArgumentNullException(nameof(entity));

            return entity.Select(i => i.MapToModel()).ToList();
        }

        /// <summary>
        /// Convert product to CartItem
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public async Task<CartItemModel> Create(Guid productId, int quantity)
        {
            quantity = Guard.Against.NegativeOrZero(quantity);
            productId = Guard.Against.Default(productId);

            var userId = _identityService.UserId!.Value;

            if (!await _context.Products.AnyAsync(p => p.Id == productId))
            {
                throw new ArgumentException($"Product id = {productId} not found", nameof(productId));
            }

            var item = await _context.CartItems.FirstOrDefaultAsync(p => p.ProductId == productId);

            if (item != null)
            {
                item.Quantity += quantity;
            }
            else
            {
                item = new CartItem
                {
                    ProductId = productId,
                    UserId = userId,
                    Quantity = quantity,
                };
                await _context.CartItems.AddAsync(item);
            }
            await _context.SaveChangesAsync();

            return (await Get(item.Id))!.MapToModel();
        }

        public async Task<CartItemModel> UpdateQuantity(Guid cartItemId, int quantity)
        {
            cartItemId = Guard.Against.Default(cartItemId);
            quantity = Guard.Against.NegativeOrZero(quantity);

            var item = await _context.CartItems.FirstOrDefaultAsync(c => c.Id == cartItemId)
                       ?? throw new ArgumentException($"CartItem id = {cartItemId} not found", nameof(cartItemId));
            item.Quantity = quantity;
            await _context.SaveChangesAsync();

            return (await Get(item.Id))!.MapToModel();
        }

        public async Task Delete(Guid cartItemId)
        {
            var item = await _context.CartItems
                .Where(i => i.Id == cartItemId)
                .FirstOrDefaultAsync();

            if (item == null)
            {
                throw new ArgumentException($"Product not found {nameof(item)}");
            }
            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
        }


        private static ProductTypeModel MapFromEntity(ProductType entity)
        {
            return new ProductTypeModel
            {
                Id = entity.Id,
                Name = entity.Name,
            };
        }
    }
}
