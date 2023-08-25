using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MobileStore.Core.Abstractions.Services;
using MobileStore.Presentation.Mvc.Controllers.Base;
using MobileStore.Presentation.Mvc.Models;
using MobileStore.Presentation.Mvc.ViewModels;

namespace MobileStore.Presentation.Mvc.Controllers
{
    public class CartController : MvcControllerBaseSecure
    {
        private readonly IMapper _mapper;
        private readonly ICartService _cartService;
        public CartController(ICartService cartService, IMapper mapper)
        {
            _cartService = cartService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var cartViewModel = new CartViewModel
            {
                CartItems = _mapper.Map<List<CartItemDto>>(await _cartService.GetCartItems()),
                ProductTypes = _mapper.Map<List<ProductTypeDto>>(await _cartService.GetProductTypes())
            };
            if (cartViewModel.CartItems == null) throw new ArgumentNullException(nameof(cartViewModel.CartItems));
            return View(cartViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Guid productId, [Range(1, int.MaxValue)] int quantity)
        {
            await _cartService.Create(productId, quantity);
            return RedirectToAction("Index", "Cart");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(Guid cartItemId, [Range(1, int.MaxValue)] int quantity)
        {
            await _cartService.UpdateQuantity(cartItemId, quantity);
            return RedirectToAction("Index", "Cart");
        }

        public async Task<IActionResult> Delete(Guid cartItemId)
        {
            try
            {
                await _cartService.Delete(cartItemId);
                return RedirectToAction("Index", "Cart");
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }
    }
}
