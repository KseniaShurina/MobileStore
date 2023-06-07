using MobileStore.Core.Models;

namespace MobileStore.Core.Abstractions.Services
{
    public interface IOrderService
    {
        Task<List<CartItemModel>> GetCartItems();
        Task<List<OrderItemModel>> GetOrderItems();
        Task RemoveOrder(int orderId);
    }
}
