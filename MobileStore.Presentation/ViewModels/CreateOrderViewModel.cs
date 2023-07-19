using MobileStore.Core.Models;

namespace MobileStore.Presentation.ViewModels
{
    public class CreateOrderViewModel
    {
        public List<CartItemModel> CartItems { get; init; } = new();

        public OrderCreateModel CreateModel { get; init; } = null!;
    }
}
