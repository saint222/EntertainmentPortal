using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using EP.Hangman.Logic.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EP.Hangman.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;

namespace EP.Hangman.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _manager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger _logger;

        public AuthController(UserManager<IdentityUser> manager, SignInManager<IdentityUser> signInManager, ILogger<AuthController> logger)
        {
            _manager = manager;
            _signInManager = signInManager;
            _logger = logger;
        }

        
        [SwaggerResponse(HttpStatusCode.OK, typeof(void), Description = "User was registered")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(IEnumerable<IdentityError>), Description = "User wasn't registered")]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserAuthData userAuthData)
        {

            var user = await _manager.FindByNameAsync(userAuthData.UserName);
            if (user == null)
            {
                var newUser = new IdentityUser()
                {
                    UserName = userAuthData.UserName
                };

                var status = await _manager.CreateAsync(newUser, userAuthData.Password);
                if (status.Succeeded)
                {
                    _logger.LogInformation($"{newUser.UserName} was registered");
                    await _signInManager.SignInAsync(newUser, false);
                }
                else
                {
                    _logger.LogWarning($"Register failed.\n{status.Errors}");
                }
                return status.Succeeded ? (IActionResult) Ok() : BadRequest(status.Errors);
            }
            else
            {
                _logger.LogWarning("Register failed. User does exist");
                return BadRequest("User does exist");
            }
        }

        [SwaggerResponse(HttpStatusCode.OK, typeof(void), Description = "User was logged in")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "User wasn't logged in")]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserAuthData userAuthData)
        {
            var result = await _signInManager.PasswordSignInAsync(userAuthData.UserName, userAuthData.Password, true, false);
            if (result.Succeeded)
            {
                _logger.LogInformation($"User {userAuthData.UserName} signed in");
            }
            else
            {
                _logger.LogWarning($"Unsuccessful login of user: {userAuthData.UserName}");
            }
            return result.Succeeded ? (IActionResult)Ok() : BadRequest();
        }

        [SwaggerResponse(HttpStatusCode.OK, typeof(void), Description = "User was logged out")]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            
            _logger.LogInformation($"User: {_signInManager.Context.User.Identity.Name} logged out ");
            return Ok();
        }
    }
}