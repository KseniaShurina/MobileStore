using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MobileStore.Core.Abstractions.Services;
using MobileStore.Core.Models;
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
            //для взаимодействия с вьюхой
            var cartViewModel = new CartViewModel();
            cartViewModel.CartItems = await _cartService.GetCartItems();
            if (cartViewModel.CartItems == null) throw new ArgumentNullException(nameof(cartViewModel.CartItems));
            return View(cartViewModel);
        }

        ///// <summary>
        ///// Добавляет товары в корзину
        ///// </summary>
        ///// <param name="productId">ид товара</param>
        ///// <param name="quantity">количество товара</param>
        ///// <returns></returns>
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
        [HttpPost]
        public async Task<IActionResult> CreatOrder(string address, string contactPhone)
        {
            try
            {
                await _cartService.CreatOrder("address", "contactPhone");
                return RedirectToAction("Index", "Order");
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            
        }
    }
}
