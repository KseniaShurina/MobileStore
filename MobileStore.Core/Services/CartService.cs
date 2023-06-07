using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using MobileStore.Common.Identity;
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

        public CartService(IDefaultContext context)
        {
            _context = context;
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
        private Task<CartItem?> Get(int id)
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
            var userId = IdentityState.Current!.UserId;
            var entity = await GetBaseQuery()
                .Where(i => i.UserId == userId)
                .ToListAsync();

            return entity.Select(i => i.MapToModel()).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public async Task<CartItemModel> Create(int productId, int quantity)
        {
            quantity = Guard.Against.NegativeOrZero(quantity);

            var userId = IdentityState.Current!.UserId;
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

        public async Task<CartItemModel> UpdateQuantity(int cartItemId, int quantity)
        {
            var item = await _context.CartItems.FirstOrDefaultAsync(c => c.Id == cartItemId);
            if (item == null)
            {
                throw new ArgumentException(nameof(cartItemId));
                //return RedirectToAction("Index", "Cart");
            }
            item.Quantity = quantity;
            await _context.SaveChangesAsync();

            return (await Get(item.Id))!.MapToModel();
        }

        public async Task Remove(int cartItemId)
        {
            var item = await _context.CartItems
                .Where(i => i.Id == cartItemId)
                .FirstOrDefaultAsync();

            if (item == null)
            {
                throw new ArgumentException($"CartItem is null here {nameof(item)}");
            }
            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<OrderModel> CreatOrder(string address, string contactPhone)
        {
            var userId = IdentityState.Current!.UserId;

            var items = await GetBaseQuery().Where(x => x.UserId == userId).ToListAsync();

            var orderItems = items
                .Select(i => new OrderItem()
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    ProductPrice = i.Product.Price * i.Quantity,
                })
                .ToList();
            //_context.CartItems.Remove(items);


            var order = new Order
            {
                Address = address,
                ContactPhone = contactPhone,
                UserId = userId,
                Datetime = DateTime.Now.ToUniversalTime(),
                Items = orderItems,
            };
            await _context.Orders.AddAsync(order);

            await _context.SaveChangesAsync();
            return order.MapToModel();
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
