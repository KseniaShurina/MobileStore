using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MobileStore.Core.Abstractions.Services;
using MobileStore.Core.Models;
using MobileStore.Presentation.Mvc.Controllers.Base;
using MobileStore.Presentation.Mvc.Models;
using MobileStore.Presentation.Mvc.ViewModels;

namespace MobileStore.Presentation.Mvc.Controllers
{
    public class OrderController : MvcControllerBaseSecure
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly ICartService _cartService;

        public OrderController(IMapper mapper, IOrderService orderService, IUserService userService, ICartService cartService)
        {
            _mapper = mapper;
            _orderService = orderService;
            _userService = userService;
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> CreateOrder()
        {
            var user = await _userService.GetCurrentUser();

            var orderViewModel = new CreateOrderViewModel
            {

                CartItems = _mapper.Map<List<CartItemDto>>(await _cartService.GetCartItems()),
                ContactInfo =
                {
                    Email = user.Email,
                    Address = user.Address,
                }
            };

            return View(orderViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderViewModel model)
        {
            //TODO ???
            try
            {
                // creating Order entity
                var orderCreate = model.ContactInfo;

                await _orderService.CreateOrder(new OrderCreateModel(
                    orderCreate.Email!,
                    orderCreate.FirstName!,
                    orderCreate.LastName!,
                    orderCreate.ContactPhone!,
                    orderCreate.Address!
                    ));

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

        public async Task<IActionResult> DeleteOrder(Guid orderId)
        {
            await _orderService.DeleteOrder(orderId);
            return RedirectToAction("Index", "Cart");
        }
    }
}
