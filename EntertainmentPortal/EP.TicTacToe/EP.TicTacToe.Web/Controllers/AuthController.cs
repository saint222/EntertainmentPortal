using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EP.TicTacToe.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _manager;

        public AuthController(UserManager<IdentityUser> manager)
        {
            _manager = manager;
        }

        [HttpGet("simple")]
        public async Task<IActionResult> SimpleLogin()
        {
            var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, "Petr Petrov"),
                    new Claim(ClaimTypes.Role, "admin")
                }, CookieAuthenticationDefaults.AuthenticationScheme,
                ClaimTypes.Name, ClaimTypes.Role);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return Ok();
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserInfo userInfo)
        {
            var newUser = new IdentityUser()
            {
                UserName = "asas",
                Email = "asasas",
                PhoneNumber = "asdasdasd"
            };
            await _manager.CreateAsync(newUser);

            var user = await _manager.FindByNameAsync(userInfo.UserName);

            if (user == null) return BadRequest("User does not exist");

            if (!await _manager.CheckPasswordAsync(user, userInfo.Password))
                return BadRequest("Username or password is incorrect");

            var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, "Ivan Ivanov"),
                    new Claim(ClaimTypes.Role, "admin"),
                    new Claim("hobby", "running")
                }, CookieAuthenticationDefaults.AuthenticationScheme,
                ClaimTypes.Name, ClaimTypes.Role);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return Ok();

            // check user logic


        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme)
                .GetAwaiter().GetResult();
            return Ok();
        }
    }

    public class UserInfo
    {
        public string UserName { get; set; }

        public string Password { get; set; }

    }
}