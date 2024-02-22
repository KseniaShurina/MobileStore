using MobileStore.Core.Models;
using MobileStore.Infrastructure.Entities;

namespace MobileStore.Core.Extensions.Entities
{
    internal static class ProductContentExtensions
    {
        public static ProductContentModel MapToModel(this ProductContent entity)
        {
            return new ProductContentModel
            {
                Id = entity.Id,
                ContentId = entity.ContentId,
                ContentType = entity.ContentType,
                Name = entity.Name,
                ProductId = entity.ProductId,
            };
        }
    }
}
