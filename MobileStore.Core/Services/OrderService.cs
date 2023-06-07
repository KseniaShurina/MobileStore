using Microsoft.EntityFrameworkCore;
using MobileStore.Common.Identity;
using MobileStore.Core.Abstractions.Services;
using MobileStore.Core.Extensions.Entities;
using MobileStore.Core.Models;
using MobileStore.Infrastructure.Abstractions.Contexts;
using MobileStore.Infrastructure.Entities;
using System.Linq;

namespace MobileStore.Core.Services
{
    internal class OrderService : IOrderService
    {
        private readonly IDefaultContext _context;

        public OrderService(IDefaultContext context)
        {
            _context = context;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IQueryable<OrderItem> GetBaseQuery()
        {
            return _context.OrderItem
                .AsNoTracking()
                .Include(p => p.Product);
        }

        private Task<OrderItem?> Get(int id)
        {
            return GetBaseQuery()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        private int GetUserId()
        {
            return IdentityState.Current!.UserId;
        }

        public async Task<List<CartItemModel>> GetCartItems()
        {
            var userId = GetUserId();
            var entity = await _context.CartItems
                .AsNoTracking()
                .Include(p => p.Product)
                .Where(u => u.UserId == userId).ToListAsync();
            return entity.Select(p => p.MapToModel()).ToList();
        }

        public async Task<List<OrderItemModel>> GetOrderItems()
        {
            var userId = GetUserId();
            var entityOrderItems = await GetBaseQuery()
                .Where(u => u.Order.UserId == userId)
                .ToListAsync();
            return entityOrderItems.Select(i => i.MapToModel()).ToList();
        }

        public async Task RemoveOrder(int orderId)
        {
            
            try
            {
                await _context.OrderItem.Where(x => x.Id == orderId).ExecuteDeleteAsync();
                //if (orderItems != null)
                //{
                //    _context.OrderItem..Remove
                    await _context.SaveChangesAsync();
                //}
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
