using MobileStore.Core.Models;

namespace MobileStore.Core.Abstractions.Services
{
    public interface IOrderService
    {
        Task<List<OrderItemModel>> GetOrderItems();
        Task DeleteOrder(Guid orderId);
        Task CreateOrder(OrderCreateModel userModel);
    }
}
