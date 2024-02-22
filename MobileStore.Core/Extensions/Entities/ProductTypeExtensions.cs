using MobileStore.Core.Models;
using MobileStore.Infrastructure.Entities;

namespace MobileStore.Core.Extensions.Entities
{
    internal static class ProductTypeExtensions
    {
        public static ProductTypeModel MapToModel(this ProductType entity)
        {
            return new ProductTypeModel
            {
                Id = entity.Id,
                Name = entity.Name,
            };
        }
    }
}
