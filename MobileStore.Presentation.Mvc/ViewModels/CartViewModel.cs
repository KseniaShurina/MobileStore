﻿using MobileStore.Presentation.Mvc.Models;

namespace MobileStore.Presentation.Mvc.ViewModels;

public class CartViewModel
{
    public List<CartItemDto> CartItems { get; set; } = new();
    public List<ProductTypeDto> ProductTypes { get; set; } = new();
}