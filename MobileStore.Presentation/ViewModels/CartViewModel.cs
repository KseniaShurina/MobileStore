using MobileStore.Core.Models;
using MobileStore.Presentation.Models;

namespace MobileStore.Presentation.ViewModels;

/// <summary>
/// Этот контроллер связывает представление с контроллером
/// </summary>
public class CartViewModel
{
    //это товары, которые передаются от контроллера в представление
    public List<CartItemDto> CartItems { get; set; } = new();
    public List<ProductTypeDto> ProductTypes { get; set; } = new();
}