using MobileStore.Core.Models;
using MobileStore.Infrastructure.Entities;

namespace MobileStore.Core.Extensions.Entities
{
    internal static class ProductExtensions
    {
        public static ProductModel MapToModel(this Product entity)
        {
            return new ProductModel
            {
                Id = entity.Id,
                ProductTypeId = entity.ProductTypeId,
                Name = entity.Name,
                Company = entity.Company,
                Price = entity.Price,
                Contents = entity.Contents.Select(i => i.MapToModel()).ToList(),
            };
        }
    }
}
