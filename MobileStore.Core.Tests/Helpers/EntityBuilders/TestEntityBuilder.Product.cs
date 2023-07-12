using MobileStore.Infrastructure.Entities;

namespace MobileStore.Core.Tests.Helpers.EntityBuilders;

internal static partial class TestEntityBuilder
{
    internal static async Task<Product> CreateProduct(this TestFixture test,
        string name, int productTypeId, int? id = null)
    {
        var entity = new Product
        {
            Name = name,
            ProductTypeId = productTypeId,
            Company = Guid.NewGuid().ToString(),
            Img = Guid.NewGuid().ToString(),
        };

        if (id != null)
        {
            entity.Id = id.Value;
        }

        test.DefaultContext.Products.Add(entity);
        await test.DefaultContext.SaveChangesAsync();

        return entity;
    }
}