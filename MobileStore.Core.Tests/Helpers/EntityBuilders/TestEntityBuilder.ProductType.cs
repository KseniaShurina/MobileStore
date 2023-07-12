using MobileStore.Infrastructure.Entities;

namespace MobileStore.Core.Tests.Helpers.EntityBuilders
{
    internal static partial class TestEntityBuilder
    {
        internal static async Task<ProductType> CreateProductType(this TestFixture test,
            string name, int? id = null)
        {
            var entity = new ProductType
            {
                Name = name,
            };

            if (id != null)
            {
                entity.Id = id.Value;
            }

            test.DefaultContext.ProductTypes.Add(entity);
            await test.DefaultContext.SaveChangesAsync();
            return entity;
        }
    }
}
