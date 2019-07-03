using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EP.WordsMaker.Logic.Models;
using EP.WordsMaker.Web.Constants;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EP.WordsMaker.Web.Controllers
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
            var identity = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "Petr Petrov"),
                    new Claim(ClaimTypes.Role, "admin")
                }, CookieAuthenticationDefaults.AuthenticationScheme,
                ClaimTypes.Name, ClaimTypes.Role);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return Ok();
        }

        [HttpGet("token")]
        public IActionResult GetToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(WordsMakerConstants.SECRET));
            var handler = new JwtSecurityTokenHandler();
            var tokenDescr = new SecurityTokenDescriptor()
            {
                Issuer = WordsMakerConstants.ISSUER_NAME,
                Audience = WordsMakerConstants.AUDIENCE_NAME,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("sub", "123-123-123-123"),
                    new Claim("name", "Ivan Ivanov"),
                }),
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddSeconds(15),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512)
            };

            var token = handler.CreateJwtSecurityToken(tokenDescr);

            return Ok(handler.WriteToken(token));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Player playerInfo)
        {

            var user = await _manager.FindByNameAsync(playerInfo.Name);

            if (user == null) return BadRequest("User does not exist");

            if (await _manager.CheckPasswordAsync(user, playerInfo.Password))
            {
                var identity = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, playerInfo.Name),
                        new Claim(ClaimTypes.Role, "admin")
                    }, CookieAuthenticationDefaults.AuthenticationScheme,
                    ClaimTypes.Name, ClaimTypes.Role);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return Ok();
            }

            // check user logic


            return BadRequest("Username or password is incorrect");
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme)
                .GetAwaiter().GetResult();
            return Ok();
        }
    }
}