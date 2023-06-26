using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MobileStore.Core.Abstractions.Services;
using MobileStore.Presentation.Controllers.Base;
using MobileStore.Presentation.ViewModels;

namespace MobileStore.Presentation.Controllers
{
    public class CartController : MvcControllerBaseSecure
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            var cartViewModel = new CartViewModel();
            cartViewModel.CartItems = await _cartService.GetCartItems();
            cartViewModel.ProductTypes = await _cartService.GetProductTypes();
            if (cartViewModel.CartItems == null) throw new ArgumentNullException(nameof(cartViewModel.CartItems));
            return View(cartViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int productId, [Range(1, int.MaxValue)] int quantity)
        {
            await _cartService.Create(productId, quantity);
            return RedirectToAction("Index", "Cart");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int cartItemId, [Range(1, int.MaxValue)] int quantity)
        {
            await _cartService.UpdateQuantity(cartItemId, quantity);
            return RedirectToAction("Index", "Cart");
        }

        public async Task<IActionResult> Remove(int cartItemId)
        {
            try
            {
                await _cartService.Remove(cartItemId);
                return RedirectToAction("Index", "Cart");
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }
    }
}
