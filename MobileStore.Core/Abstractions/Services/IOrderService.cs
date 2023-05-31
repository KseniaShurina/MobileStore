using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileStore.Core.Models;

namespace MobileStore.Core.Abstractions.Services
{
    public interface IOrderService
    {
        Task<List<OrderItemModel>> GetOrderItems();
        Task RemoveOrder(int orderId);
    }
}
