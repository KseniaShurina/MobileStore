using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using MobileStore.Common.Identity;
using MobileStore.Core.Abstractions.Services;
using MobileStore.Core.Extensions.Entities;
using MobileStore.Core.Models;
using MobileStore.Infrastructure.Contexts;
using MobileStore.Infrastructure.Entities;

namespace MobileStore.Core.Services
{
    internal class CartService : ICartService
    {
        private readonly DefaultContext _context;

        public CartService(DefaultContext context)
        {
            _context = context;
        }

        private IQueryable<CartItem> GetBaseQuery()
        {
            return _context.CartItems
                .AsNoTracking()
                .Include(i => i.Product);
        }

        private Task<CartItem?> Get(int id)
        {
            return GetBaseQuery()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<CartItemModel>> GetCartItems()
        {
            var userId = IdentityState.Current!.UserId;
            var entity = await GetBaseQuery()
                .Where(i => i.UserId == userId)
                .ToListAsync();

            return entity.Select(i => i.MapToModel()).ToList();
        }

        public async Task <CartItemModel> Create(int productId, int quantity)
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

        public async Task <CartItemModel> UpdateQuantity(int cartItemId, int quantity)
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

        public async Task Remove(int itemId)
        {
            var item = await _context.CartItems.FirstOrDefaultAsync(cart => cart.ProductId == itemId);
            if (item == null)
            {
                //return NotFound();
                throw new ArgumentException(nameof(item));
            }
            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<CartItemModel> CreateOrder()
        {
            throw new NotImplementedException();
        }
    }
}
