using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobileStore.Core.Abstractions.Services;
using MobileStore.Core.Models;
using MobileStore.Presentation.Blazor.ViewModels;

// пространство имен моделей RegisterModel и LoginModel
// пространство имен UserContext и класса User

namespace MobileStore.Presentation.Blazor.Controllers.Mvc
{
    public class AccountController : Controller
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
                // добавляем пользователя в бд
                var user = await _accountService.RegisterUser(new UserRegisterModel(model.Email!, model.Password!));
                // аутентификация
                await Authenticate(user);

                return RedirectTo("/");
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

            // creating ClaimsIdentity object
            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            // setting authentication cookies
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectTo("/");
        }

        private IActionResult RedirectTo(string? returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectTo("/");
        }
    }
}
