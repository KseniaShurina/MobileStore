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
            orderViewModel.OrderItems = await _orderService.GetOrderItems();
            if (orderViewModel.OrderItems == null) throw new ArgumentNullException(nameof(orderViewModel.OrderItems));

            return View(orderViewModel);
        }
    }
}
