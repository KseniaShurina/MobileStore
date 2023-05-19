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
            return await Index();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int cartItemId, [Range(1, int.MaxValue)] int quantity)
        {
            await _cartService.UpdateQuantity(cartItemId, quantity);
            return RedirectToAction("Index", "Cart");
        }

        public async Task<IActionResult> Remove(int cartItemId)
        {
            await _cartService.Remove(cartItemId);
            //if (item == null) { return NotFound();}
            return RedirectToAction("Index", "Cart");
        }

        //public async Task<IActionResult> CreateOrder()
        //{
        //    var cartItems = await _context.CartItems.Where(c => c.UserId == UserId).ToListAsync();

        //    var order = new Order()
        //    {
        //        Address = "test",
        //        ContactPhone = "+123 45 678 9",
        //        UserId = UserId,
        //        Items = cartItems.Select(i => new OrderItem
        //            {
        //                ProductId = i.ProductId,
        //                Quantity = i.Quantity,
        //            })
        //            .ToList()
        //    };

        //    _context.Orders.Add(order);
        //    _context.CartItems.RemoveRange(cartItems);

        //    await _context.SaveChangesAsync();

        //    return null;
        //}
    }
}
