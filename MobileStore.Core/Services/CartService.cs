using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using MobileStore.Common.Abstractions.Services;
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
                .Include(i => i.Product)
                .ThenInclude(i => i.Contents);
        }

        private Task<CartItem?> Get(Guid cartItemId)
        {
            return GetBaseQuery()
                .Where(i => i.Id == cartItemId)
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
                throw new ArgumentException($"Product cartItemId = {productId} not found", nameof(productId));
            }

            var item = await _context.CartItems
                .FirstOrDefaultAsync(p => p.ProductId == productId && p.UserId == userId);

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
                       ?? throw new ArgumentException($"CartItem cartItemId = {cartItemId} not found", nameof(cartItemId));
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
