using EP.Balda.Logic.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace EP.Balda.Web.Controllers
{
    [ApiController]
    public class AutheticationController : ControllerBase
    {
        private readonly UserManager<Player> _manager;
        private readonly SignInManager<Player> _signInManager;
        private readonly ILogger _logger;

        public AutheticationController(UserManager<Player> manager, SignInManager<Player> signInManager, ILogger<AutheticationController> logger)
        {
            _manager = manager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [SwaggerResponse(HttpStatusCode.OK, typeof(void), Description = "User has been registered")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(IEnumerable<IdentityError>), Description = "User wasn't registered")]
        [HttpPost("api/register")]
        public async Task<IActionResult> Register([FromBody] User userData)
        {
            var user = await _manager.FindByNameAsync(userData.UserName);

            if (user == null)
            {
                var newUser = new Player()
                {
                    UserName = userData.UserName
                };

                var status = await _manager.CreateAsync(newUser, userData.Password);

                if (status.Succeeded)
                {
                    _logger.LogInformation($"{newUser.UserName} was registered");
                    await _signInManager.SignInAsync(newUser, false);
                }
                else
                {
                    _logger.LogWarning($"Register failed.\n{status.Errors}");
                }

                return status.Succeeded ? (IActionResult)Ok() : BadRequest(status.Errors);
            }
            else
            {
                _logger.LogWarning($"Register failed. User {userData.UserName} does exist");
                return BadRequest($"User {userData.UserName} already exists");
            }
        }

        [SwaggerResponse(HttpStatusCode.OK, typeof(void), Description = "User was logged in")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "User wasn't logged in")]
        [HttpPost("api/login")]
        public async Task<IActionResult> Login([FromBody] User userData)
        {
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