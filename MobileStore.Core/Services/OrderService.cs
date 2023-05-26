using Microsoft.EntityFrameworkCore;
using MobileStore.Common.Identity;
using MobileStore.Core.Abstractions.Services;
using MobileStore.Core.Extensions.Entities;
using MobileStore.Core.Models;
using MobileStore.Infrastructure.Contexts;
using MobileStore.Infrastructure.Entities;

namespace MobileStore.Core.Services
{
    internal class OrderService : IOrderService
    {
        private readonly DefaultContext _context;

        public OrderService(DefaultContext context)
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
    }
}
