using MobileStore.Core.Models;

namespace MobileStore.Presentation.ViewModels
{
    public class OrderViewModel
    {
        //это товары, которые передаются от контроллера в представление
        public List<OrderItemModel> OrderItems { get; set; } = new();
        public List<CartItemModel> CartItems { get; set; } = new();
    }
}
