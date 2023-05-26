using MobileStore.Core.Models;

namespace MobileStore.Presentation.ViewModels
{/// <summary>
/// Этот контроллер связывает представление с контроллером
/// </summary>
    public class CartViewModel
    {
        //это товары, которые передаются от контроллера в представление
        public List<CartItemModel> CartItems { get; set; } = new();
    }
}
