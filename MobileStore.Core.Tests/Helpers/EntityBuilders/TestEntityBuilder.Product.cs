using MobileStore.Infrastructure.Entities;

namespace MobileStore.Core.Tests.Helpers.EntityBuilders;

internal static partial class TestEntityBuilder
{
    internal static async Task<Product> CreateProduct(this TestFixture test,
        string name, Guid productTypeId)
    {
        var entity = new Product
        {
            Id = Guid.NewGuid(),
            Name = name,
            ProductTypeId = productTypeId,
            Company = Guid.NewGuid().ToString(),
            Img = Guid.NewGuid().ToString(),
        };

        test.DefaultContext.Products.Add(entity);
        await test.DefaultContext.SaveChangesAsync();

        return entity;
    }
}