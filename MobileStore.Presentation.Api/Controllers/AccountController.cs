﻿using System.ComponentModel.DataAnnotations;
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
        private readonly IConfiguration _configuration;

        public AccountController(IAccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
        }

        /// <summary>
        /// User Authenticate
        /// </summary>
        /// <param name="model">ViewModel for transfer user data</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login([Required]LoginDto model)
        {
            var validUser = await _accountService.IsValidPassword(model.Email, model.Password);
            if (validUser)
            {
                var user = await _accountService.GetUserByEmail(model.Email!);

                var jwt = Helpers.JwtHelper.CreateJwt(user!, _configuration);

                return Ok(jwt);
            }
            return BadRequest();
        }
    }
}
