﻿using Microsoft.EntityFrameworkCore;
using MobileStore.Common.Abstractions.Services;
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
        private readonly IReadIdentityService _identityService;

        public OrderService(IDefaultContext context, IReadIdentityService identityService)
        {
            _context = context;
            _identityService = identityService;
        }

        private Guid GetUserId()
        {
            return _identityService.UserId!.Value;;
        }

        public async Task<OrderModel> CreateOrder(OrderCreateModel orderCreateModel)
        {
            var userId = GetUserId();

            await using var transaction = await _context.BeginTransactionAsync();

            try
            {
                var cartItems = await _context.CartItems
                    .Include(p => p.Product)
                    .Where(u => u.UserId == userId)
                    .ToListAsync();

                if (!cartItems.Any())
                {
                    throw new ArgumentException("Cart items does not exists");
                }

                var orderItems = cartItems
                    .Select(i => new OrderItem
                    {
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        ProductPrice = i.Product.Price,
                    })
                    .ToList();

                var order = new Order
                {
                    Datetime = DateTime.Now.ToUniversalTime(),
                    FirstName = orderCreateModel.FirstName,
                    LastName = orderCreateModel.LastName,
                    Address = orderCreateModel.Address,
                    ContactPhone = orderCreateModel.ContactPhone,
                    UserId = userId,
                    Items = orderItems,
                };

                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();

                _context.CartItems.RemoveRange(cartItems);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return (await GetOrder(order.Id))!;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<OrderModel?> GetOrder(Guid orderId)
        {
            var userId = GetUserId();
            var order = await _context.Orders
                .Where(i => i.UserId == userId && i.Id == orderId)
                .Include(i => i.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync();

            return order?.MapToModel();
        }

        public async Task<List<OrderModel>> GetOrders()
        {
            var userId = GetUserId();
            var orders = await _context.Orders
                .Where(u => u.UserId == userId)
                .ToListAsync();
            return orders.Select(i => i.MapToModel()).ToList();
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
    }
}
