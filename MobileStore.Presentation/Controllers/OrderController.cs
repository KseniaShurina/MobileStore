using Microsoft.AspNetCore.Mvc;
using MobileStore.Presentation.Controllers.Base;
using MobileStore.Presentation.ViewModels;
using MobileStore.Core.Abstractions.Services;
using MobileStore.Core.Models;

namespace MobileStore.Presentation.Controllers
{
    public class OrderController : MvcControllerBaseSecure
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly ICartService _cartService;

        public OrderController(IOrderService orderService, IUserService userService, ICartService cartService)
        {
            _orderService = orderService;
            _userService = userService;
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> CreateOrder()
        {
            var orderViewModel = new OrderViewModel();
            orderViewModel.CartItems = await _cartService.GetCartItems();
            orderViewModel.User = await _userService.GetCurrentUser();

            return View(orderViewModel);
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderCreateModel model)
        {
            try
            {
                await _orderService.CreateOrder(model);
                return RedirectToAction("OrderCreated", "Order");
            }
            catch (Exception)
            {
                return NotFound();
            }

        }

        [HttpGet]
        public IActionResult OrderCreated()
        {
            return View();
        }

        public async Task<IActionResult> RemoveOrder(Guid orderId)
        {
            await _orderService.DeleteOrder(orderId);
            return RedirectToAction("Index", "Cart");
        }
    }
}
