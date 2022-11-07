using System.Collections.Generic;
using MobileStore.Infrastructure.Models;

namespace MobileStore.Presentation.ViewModels
{/// <summary>
/// Этот контроллер связывает представление с контроллером
/// </summary>
    public class CartViewModel
    {
        //это товары, которые передаются от контроллера в представление
        public List<CartItem> CartItems { get; set; } = new();
    }
}
