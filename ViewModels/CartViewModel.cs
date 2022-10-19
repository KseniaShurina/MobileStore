using System.Collections.Generic;
using MobileStore.Models;

namespace MobileStore.ViewModels
{/// <summary>
/// Этот контроллер связывает представление с контроллером
/// </summary>
    public class CartViewModel
    {
        //это товары, которые передаются от контроллера в представление
        public List<CartItem> CartItems { get; set; } = new();
    }
}
