using MobileStore.Infrastructure.Entities;

namespace MobileStore.Core.Tests.Helpers.EntityBuilders
{
    internal static partial class TestEntityBuilder
    {
        internal static async Task<CartItem> CreateCartItem(this TestFixture test,
            Guid productId, int quantity, Guid? userId = null)
        {
            var entity = new CartItem
            {
                ProductId = productId,
                Quantity = quantity,
                UserId = userId ?? test.UserId,
            };

            await test.DefaultContext.AddAsync(entity);
            await test.DefaultContext.SaveChangesAsync();

            return entity;
        }
    }
}
