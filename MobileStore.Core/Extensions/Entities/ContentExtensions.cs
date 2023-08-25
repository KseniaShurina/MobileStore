using MobileStore.Core.Models;
using MobileStore.Infrastructure.Entities;

namespace MobileStore.Core.Extensions.Entities
{
    internal static class ContentExtensions 
    {
        public static ContentModel MapToModel(this Content entity)
        {
            return new ContentModel()
            {
                Id = entity.Id,
                ContentType = entity.ContentType,
                Name = entity.Name,
                Data = entity.Data,
            };
        }
    }
}
