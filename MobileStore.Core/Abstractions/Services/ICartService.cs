using MobileStore.Core.Models;

namespace MobileStore.Core.Abstractions.Services;

public interface ICartService
{
    Task<List<CartItemModel>> GetCartItems();

    /// <summary>
    /// Добавляет товары в корзину
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="quantity"></param>
    /// <returns></returns>
    Task<CartItemModel> Create(int productId, int quantity);

    Task<CartItemModel> UpdateQuantity(int cartItemId, int quantity);

    Task Remove(int cartItemId);

    Task<OrderModel> CreatOrder(string address, string contactPhone);
}