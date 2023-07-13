using MobileStore.Core.Models;

namespace MobileStore.Core.Abstractions.Services;

public interface ICartService

{
    Task<List<ProductTypeModel>> GetProductTypes();

    Task<List<CartItemModel>> GetCartItems();

    /// <summary>
    /// Добавляет товары в корзину
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="quantity"></param>
    /// <returns></returns>
    Task<CartItemModel> Create(Guid productId, int quantity);

    Task<CartItemModel> UpdateQuantity(Guid cartItemId, int quantity);

    Task Delete(Guid cartItemId);
}