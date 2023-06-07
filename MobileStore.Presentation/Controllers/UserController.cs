using MobileStore.Core.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;
using MobileStore.Presentation.Controllers.Base;
using MobileStore.Presentation.ViewModels;

namespace MobileStore.Presentation.Controllers
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
