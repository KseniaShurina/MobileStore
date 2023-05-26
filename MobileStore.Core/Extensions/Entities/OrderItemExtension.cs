using MobileStore.Core.Models;
using MobileStore.Infrastructure.Entities;

namespace MobileStore.Core.Extensions.Entities
{
    internal static class OrderItemExtension
    {
        public static OrderItemModel MapToModel(this OrderItem entity)
        {
            return new OrderItemModel
            {
                Id = entity.Id,
                OrderId = entity.OrderId,
                Order = entity.Order,
                ProductId = entity.ProductId,
                Product = entity.Product/*.MapToModel()*/,
                Quantity = entity.Quantity,
            };
        }
    }
}
