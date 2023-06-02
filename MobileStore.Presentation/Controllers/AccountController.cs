using System.Security.Claims;
using MobileStore.Core.Abstractions.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobileStore.Core.Models;
using MobileStore.Presentation.Controllers.Base;
using MobileStore.Presentation.ViewModels;
// пространство имен моделей RegisterModel и LoginModel
// пространство имен UserContext и класса User

namespace MobileStore.Presentation.Controllers
{
    public class AccountController : MvcControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        /// <summary>
        /// возвращает представление с формой
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {
            if (HttpContext.User.Identity?.IsAuthenticated ?? false)
            {
                return RedirectTo(returnUrl);
            }

            var model = new LoginViewModel { ReturnUrl = returnUrl };
            return View(model);
        }
        /// <summary>
        /// Обращаемся в БД для аутентификации пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // ! shows that item cannot be null here
            var validUser = await _accountService.IsValidPassword(model.Email!, model.Password!);
            if (validUser)
            {
                var user = await _accountService.GetUserByEmail(model.Email!);
                await Authenticate(user!);

                return RedirectTo(model.ReturnUrl);


            }
            ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel? model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            if (ModelState.IsValid)
            {
                // Достаем пользователя по почте
                var user = await _accountService.GetUserByEmail(model.Email!);

                if (user == null) throw new ArgumentNullException($"{nameof(user)}", "Пользователь с такими параметрами уже существует");
                // добавляем пользователя в бд
                await _accountService.RegisterUser(user);
                // аутентификация
                await Authenticate(user);

                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        /// <summary>
        /// Claim is stored information in Cookie
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task Authenticate(UserModel user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email!),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        private IActionResult RedirectTo(string? returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
