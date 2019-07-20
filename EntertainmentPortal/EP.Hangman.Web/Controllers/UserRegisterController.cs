using System.Threading.Tasks;
using EP.Hangman.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EP.Hangman.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegisterController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _manager;
        private readonly ILogger _logger;

        public UserRegisterController (UserManager<IdentityUser> manager, ILogger<UserRegisterController> logger)
        {
            _manager = manager;
            _logger = logger;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterWithToken([FromBody]UserAuthData userAuthData)
        {
            
            var user = await _manager.FindByEmailAsync(userAuthData.Email);
            if (user == null)
            {
                
                var newUser = new IdentityUser()
                {
                    UserName = userAuthData.UserName,
                    Email = userAuthData.Email
                };

                var status = await _manager.CreateAsync(newUser, userAuthData.Password);
                if (status.Succeeded)
                {
                    _logger.LogInformation($"{newUser.UserName} was registered");
                    user = await _manager.FindByEmailAsync(userAuthData.Email);
                    return Ok();

                }
                else
                {
                    _logger.LogWarning($"Register failed.\n{status.Errors}");
                    return BadRequest(status.Errors);
                }
            }
            else
            {
                _logger.LogWarning("Register failed. User does exist");
                return BadRequest("User does exist");
            }
            
        }
    }
}