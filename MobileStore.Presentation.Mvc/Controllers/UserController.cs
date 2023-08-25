using Microsoft.AspNetCore.Mvc;
using MobileStore.Core.Abstractions.Services;
using MobileStore.Presentation.Mvc.Controllers.Base;
using MobileStore.Presentation.Mvc.ViewModels;

namespace MobileStore.Presentation.Mvc.Controllers
{
    public class UserController : MvcControllerBaseSecure
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            var userViewModel = new UserViewModel
            {
                User = await _userService.GetCurrentUser()
            };
            return View(userViewModel);
        }

        public async Task<IActionResult> UpdateCurrentUser(UserViewModel model)
        {
            await _userService.UpdateCurrentUser(model.User);

            return RedirectToAction("Index", "User");
        }
    }
}
