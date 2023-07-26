﻿using MobileStore.Presentation.Models;
using System.ComponentModel.DataAnnotations;

namespace MobileStore.Presentation.ViewModels
{
    public class CreateOrderViewModel
    {
        public List<CartItemDto> CartItems { get; init; } = new();

        public CreateOrderContactInfoDto ContactInfo { get; } = new();

        public class CreateOrderContactInfoDto
        {
            [Required]
            public string? Email { get; set; }

            public string? FirstName { get; set; }

            public string? LastName { get; set; }

            public string? ContactPhone { get; set; }

            public string? Address { get; set; }
        }
    }
}
