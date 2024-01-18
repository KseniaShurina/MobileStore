using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MobileStore.Core.Abstractions.Services;
using MobileStore.Presentation.Api.Models;

namespace MobileStore.Presentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// User Authenticate
        /// </summary>
        /// <param name="model">ViewModel for transfer user data</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Required]LoginDto model)
        {
            var validUser = await _accountService.IsValidPassword(model.Email, model.Password);
            if (validUser)
            {
                var user = await _accountService.GetUserByEmail(model.Email!);

                var jwt = Helpers.JwtHelper.CreateJwt(user!);

                return Ok(jwt);
            }
            return BadRequest();
        }
    }
}
