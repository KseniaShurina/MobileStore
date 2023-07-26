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
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                Address = entity.Address,
                ContactPhone = entity.ContactPhone,
                UserId = entity.UserId,
                Items = entity.Items.Select(i => i.MapToModel()).ToList(),
                Datetime = entity.Datetime,
            };
        }
    }
}
