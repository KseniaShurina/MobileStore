using Microsoft.AspNetCore.Mvc;
using MobileStore.Presentation.Controllers.Base;
using MobileStore.Presentation.ViewModels;
using MobileStore.Core.Abstractions.Services;

namespace MobileStore.Presentation.Controllers
{
    public class OrderController : MvcControllerBaseSecure
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<IActionResult> Index()
        {
            var orderViewModel = new OrderViewModel();
            orderViewModel.CartItems = await _orderService.GetCartItems();
            orderViewModel.OrderItems = await _orderService.GetOrderItems();
            if (orderViewModel.OrderItems == null) throw new ArgumentNullException(nameof(orderViewModel.OrderItems));

            return View(orderViewModel);
        }

        public async Task<IActionResult> RemoveOrder(int orderId)
        {
            await _orderService.RemoveOrder(orderId);
            return RedirectToAction("Index", "Cart");
        }
    }
}
