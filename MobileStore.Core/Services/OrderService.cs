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

        private IQueryable<OrderItem> GetBaseQuery()
        {
            return _context.OrderItem
                .AsNoTracking()
                .Include(p => p.Product);
        }

        private Guid GetUserId()
        {
            return IdentityState.Current!.UserId;
        }

        public async Task<List<OrderItemModel>> GetOrderItems()
        {
            var userId = GetUserId();
            var entityOrderItems = await GetBaseQuery()
                .Where(u => u.Order.UserId == userId)
                .ToListAsync();
            return entityOrderItems.Select(i => i.MapToModel()).ToList();
        }

        public async Task DeleteOrder(Guid orderId)
        {
            try
            {
                await _context.OrderItem.Where(x => x.Id == orderId).ExecuteDeleteAsync();
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task CreateOrder(OrderCreateModel model)
        {
            var userId = GetUserId();

            await using var transaction = await _context.BeginTransactionAsync();

            try
            {
                var cartItems = await _context.CartItems
                    .Include(p => p.Product)
                    .Where(u => u.UserId == userId).ToListAsync();

                var orderItems = cartItems
                    .Select(i => new OrderItem
                    {
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        ProductPrice = i.Product.Price * i.Quantity,
                    })
                    .ToList();

                var order = new Order
                {
                    Datetime = DateTime.Now.ToUniversalTime(),
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address!,
                    ContactPhone = model.ContactPhone,
                    UserId = userId,  
                    Items = orderItems,
                };
                await _context.Orders.AddAsync(order);

                await _context.CartItems.Where(c => c.UserId == userId).ExecuteDeleteAsync();

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
