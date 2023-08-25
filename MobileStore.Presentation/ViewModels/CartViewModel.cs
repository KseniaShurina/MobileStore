using MobileStore.Presentation.Models;

namespace MobileStore.Presentation.ViewModels;

public class CartViewModel
{
    public List<CartItemDto> CartItems { get; set; } = new();
    public List<ProductTypeDto> ProductTypes { get; set; } = new();
}