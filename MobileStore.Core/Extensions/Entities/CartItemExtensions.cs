using MobileStore.Core.Models;
using MobileStore.Infrastructure.Entities;

namespace MobileStore.Core.Extensions.Entities
{
    internal static class CartItemExtensions
    {
        public static CartItemModel MapToModel(this CartItem entity)
        {
            return new CartItemModel
            {
                Id = entity.Id,
                UserId = entity.UserId,
                ProductId = entity.ProductId,
                Product = entity.Product.MapToModel(),
                Quantity = entity.Quantity,
            };
        }
    }
}
