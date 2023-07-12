using MobileStore.Infrastructure.Entities;

namespace MobileStore.Core.Tests.Helpers.EntityBuilders
{
    internal static partial class TestEntityBuilder
    {
        internal static async Task<CartItem> CreateCartItem(this TestFixture test,
            int productId, int quantity, int? id = null, int? userId = null)
        {
            var entity = new CartItem
            {
                ProductId = productId,
                Quantity = quantity,
            };

            if (id != null)
            {
                entity.Id = id.Value;
            }
            if (userId != null)
            {
                entity.UserId = userId.Value;
            }

            await test.DefaultContext.AddAsync(entity);
            await test.DefaultContext.SaveChangesAsync();

            return entity;
        }
    }
}
