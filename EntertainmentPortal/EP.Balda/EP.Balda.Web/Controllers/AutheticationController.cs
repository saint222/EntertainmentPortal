using EP.Balda.Data.Models;
using EP.Balda.Logic.Models;
using EP.Balda.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace EP.Balda.Web.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<PlayerDb> _userManager;
        private readonly SignInManager<PlayerDb> _signInManager;
        private readonly ILogger _logger;

        public AuthenticationController(UserManager<PlayerDb> manager, SignInManager<PlayerDb> signInManager, ILogger<AuthenticationController> logger)
        {
            _userManager = manager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [SwaggerResponse(HttpStatusCode.OK, typeof(void), Description = "User has been registered")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(IEnumerable<IdentityError>), Description = "User wasn't registered")]
        [HttpPost("api/register")]
        public async Task<IActionResult> Register([FromBody] UserRegistration userData)
        {
            var user = await _userManager.FindByNameAsync(userData.UserName);

            if(userData.Password != userData.PasswordConfirm)
            {
                return BadRequest($"Passwords don't match"); //add validator
            }

            if (user != null)
            {
                _logger.LogWarning($"Register failed. User {userData.UserName} does exist");
                return BadRequest($"User {userData.UserName} already exists");
            }
            
            var newUser = new PlayerDb()
            {
                UserName = userData.UserName,
                PasswordHash = _userManager.PasswordHasher.HashPassword(user, userData.Password),
                Email = userData.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var status = await _userManager.CreateAsync(newUser, userData.Password);

            if (status.Succeeded)
            {
                _logger.LogInformation($"{newUser.UserName} was registered");

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

                var callbackUrl = Url.Action(
                       "ConfirmEmail",
                       "Authentication",
                       new { userId = newUser.Id, code = code },
                       protocol: HttpContext.Request.Scheme);
                var emailService = new EmailService();
                await emailService.SendEmailAsync(userData.Email, "Confirm your account",
                    $"Dear {userData.UserName}, please, confirm your registration by clicking on the link: <a href='{callbackUrl}'>link</a>" +
                    $"With best regards, Entertainment Portal Administration");

                return Content("To complete registration, please check your e-mail and follow the link provided in the letter");
            }
            else
            {
                _logger.LogWarning($"Register failed.\n{status.Errors}");
            }

            return status.Succeeded ? (IActionResult)Ok() : BadRequest(status.Errors);
        }

        [SwaggerResponse(HttpStatusCode.OK, typeof(void), Description = "User was logged in")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "User wasn't logged in")]
        [HttpPost("api/login")]
        public async Task<IActionResult> Login([FromBody] UserLogin userData)
        {
            var user = await _userManager.FindByNameAsync(userData.UserName);

            if (user != null)
            {
                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    return BadRequest("You didn't confirm your e-mail");
                }
            }

            var result = await _signInManager.PasswordSignInAsync(userData.UserName, userData.Password, true, false);

            if (result.Succeeded)
            {
                _logger.LogInformation($"User {userData.UserName} signed in");
            }
            else
            {
                _logger.LogWarning($"Unsuccessful login of user: {userData.UserName}");
            }

            return result.Succeeded ? (IActionResult)Ok() : BadRequest();
        }

        [SwaggerResponse(HttpStatusCode.OK, typeof(void), Description = "User confirmed e-mail")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "User didn't confirm e-mail")]
        [HttpGet("api/confirm")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return BadRequest("Error");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return BadRequest("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Error");
            }
        }

        [SwaggerResponse(HttpStatusCode.OK, typeof(void), Description = "User was logged out")]
        [HttpPost("api/logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            _logger.LogInformation($"User: {_signInManager.Context.User.Identity.Name} logged out ");
            return Ok();
        }
    }
}