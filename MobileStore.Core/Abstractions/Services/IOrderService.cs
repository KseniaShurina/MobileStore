using MobileStore.Core.Models;

namespace MobileStore.Core.Abstractions.Services
{
    public interface IOrderService
    {
        /// <summary>
        /// SaveFileToDatabase new order if data base doesn't have order
        /// </summary>
        /// <param name="orderCreateModel">Object for transfer the data</param>
        /// <returns></returns>
        Task<OrderModel> CreateOrder(OrderCreateModel orderCreateModel);
        
        ///// <summary>
        ///// Return order items from current order
        ///// </summary>
        ///// <returns></returns>
        //Task<List<OrderItemModel>> GetOrderItems();

        /// <summary>
        /// Return current order by order id
        /// </summary>
        /// <param name="orderId">Order id</param>
        /// <returns></returns>
        Task<OrderModel?> GetOrder(Guid orderId);

        /// <summary>
        /// Return whole orders of user
        /// </summary>
        /// <returns></returns>
        Task<List<OrderModel>> GetOrders();

        /// <summary>
        /// Delete current order by order id
        /// </summary>
        /// <param name="orderId">Order id</param>
        /// <returns></returns>
        Task DeleteOrder(Guid orderId);
    }
}
