using System.Security.Claims;
using System.Threading.Tasks;
using EP.Hangman.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace EP.Hangman.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private const string AUTH_ROLE_USER = "User";
        private readonly UserManager<IdentityUser> _manager;

        public AuthController(UserManager<IdentityUser> manager)
        {
            _manager = manager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserAuthData userAuthData)
        {

            var user = await _manager.FindByNameAsync(userAuthData.UserName);
            if (user == null)
            {
                var newUser = new IdentityUser()
                {
                    UserName = userAuthData.UserName,
                    PasswordHash = userAuthData.Password
                };

                await _manager.CreateAsync(newUser);
                return Ok();
            }
            else
            {
                return BadRequest("User does exist");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserAuthData userAuthData)
        {

            var user = await _manager.FindByNameAsync(userAuthData.UserName);
            if (user != null)
            {
                if (await _manager.CheckPasswordAsync(user, userAuthData.Password))
                {
                    var identity = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Name, userAuthData.UserName),
                            new Claim(ClaimTypes.Role, AUTH_ROLE_USER),
                        }, CookieAuthenticationDefaults.AuthenticationScheme,
                        ClaimTypes.Name, ClaimTypes.Role);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    return Ok();
                }
                else
                {
                    return BadRequest("Wrong password");
                }
            }
            else
            {
                return BadRequest("User wasn't found");
            }
        }
    }
}