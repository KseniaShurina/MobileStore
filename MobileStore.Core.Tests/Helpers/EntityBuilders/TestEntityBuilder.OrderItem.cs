using MobileStore.Infrastructure.Entities;

namespace MobileStore.Core.Tests.Helpers.EntityBuilders
{
    internal static partial class TestEntityBuilder
    {
        internal static async Task<OrderItem> CreateOrderItem(this TestFixture test,
             int quantity, Guid? productId = null, Guid? orderId = null)
        {
            var entity = new OrderItem()
            {
                Quantity = quantity,
                ProductId = Guid.NewGuid(),
                OrderId = orderId ?? Guid.NewGuid(),
            };

            await test.DefaultContext.AddAsync(entity);
            await test.DefaultContext.SaveChangesAsync();

            return entity;
        }
    }
}
