using Microsoft.EntityFrameworkCore;
using MobileStore.Common.Identity;
using MobileStore.Core.Abstractions.Services;
using MobileStore.Core.Extensions.Entities;
using MobileStore.Core.Models;
using MobileStore.Infrastructure.Abstractions.Contexts;
using MobileStore.Infrastructure.Entities;

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

        public async Task<List<OrderItemModel>> GetOrderItems()
        {
            var userId = IdentityState.Current!.UserId;
            var entityOrderItems = await GetBaseQuery()
                .Where(u => u.Order.UserId == userId)
                .ToListAsync();
            return entityOrderItems.Select(i => i.MapToModel()).ToList();
        }

        public async Task RemoveOrder(int orderId)
        {
            var order = await _context.Orders.Where(x => x.Id == orderId).FirstOrDefaultAsync();
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"Order is null {nameof(order)}");
            }
        }
    }
}
