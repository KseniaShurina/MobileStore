using MobileStore.Core.Models;
using MobileStore.Infrastructure.Entities;

namespace MobileStore.Core.Extensions.Entities
{
    internal static class ProductExtensions
    {
        public static ProductModel MapToModel(this Product entity) // entity was null
        {
            return new ProductModel
            {
                Id = entity.Id,
                ProductTypeId = entity.ProductTypeId,
                Name = entity.Name,
                Company = entity.Company,
                Img = entity.Img,
                Price = entity.Price,
            };
        }
    }
}
