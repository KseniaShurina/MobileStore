using MobileStore.Core.Models;

namespace MobileStore.Presentation.ViewModels
{
    public class OrderViewModel
    {
        public List<CartItemModel> CartItems { get; set; } = new();

        public UserModel User { get; set; } = null!;
        public OrderCreateModel CreateModel { get; set; } = null!;
    }
}
