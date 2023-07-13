using MobileStore.Infrastructure.Entities;

namespace MobileStore.Core.Tests.Helpers.EntityBuilders
{
    internal static partial class TestEntityBuilder
    {
        internal static async Task<ProductType> CreateProductType(this TestFixture test,
            string name)
        {
            var entity = new ProductType
            {
                Id = Guid.NewGuid(),
                Name = name,
            };

            test.DefaultContext.ProductTypes.Add(entity);
            await test.DefaultContext.SaveChangesAsync();
            return entity;
        }
    }
}
