using MobileStore.Core.Models;
using MobileStore.Infrastructure.Entities;

namespace MobileStore.Core.Extensions.Models
{
    internal static class ProductContentModelExtensions
    {
        internal static ProductContent MapToEntity(this ProductContentModel model)
        {
            return new ProductContent
            {
                Id = model.Id,
                ContentId = model.ContentId,
                ContentType = model.ContentType,
                Name = model.Name,
                ProductId = model.ProductId,
            };
        }
    }
}
