using MobileStore.Core.Models;
using MobileStore.Infrastructure.Entities;

namespace MobileStore.Core.Extensions.Entities
{
    internal static class OrderExtension
    {
        public static OrderModel MapToModel(this Order entity)
        {
            return new OrderModel
            {
                Id = entity.Id,
                Address = entity.Address,
                ContactPhone = entity.ContactPhone,
                UserId = entity.UserId,
                User = entity.User,
                Items = entity.Items,
                Datetime = entity.Datetime,
            };
        }
    }
}
