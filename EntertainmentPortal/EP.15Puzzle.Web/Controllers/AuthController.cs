using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EP._15Puzzle.Logic.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace EP._15Puzzle.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        /*
        private readonly UserManager<IdentityUser> _manager;

        public AuthController(UserManager<IdentityUser> manager)
        {
            _manager = manager;
        }
        */

        /*
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserInfo userInfo)
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

            if (await _manager.CheckPasswordAsync(user, userInfo.Password))
            {
                var identity = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, "Ivan Ivanov"),
                        new Claim(ClaimTypes.Role, "admin"),
                        new Claim("hobby", "running")
                    }, CookieAuthenticationDefaults.AuthenticationScheme,
                    ClaimTypes.Name, ClaimTypes.Role);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return Ok();
            }

            // check user logic


            return BadRequest("Username or password is incorrect");
        }
        */

        [HttpPost("login")]
        public IActionResult LoginByCredentials([FromBody] UserCred userInfo)
        {
            var jwt = new {jwt = getToken().RawData};
            Response.WriteAsync(JsonConvert.SerializeObject(jwt));
            return Ok();
        }

        [Authorize(AuthenticationSchemes = "Facebook")]
        [HttpGet("facebook")]
        public IActionResult LoginByFacebook()
        {
            var jwt = new { jwt = getToken().RawData };
            return Ok(JsonConvert.SerializeObject(jwt));
        }

        [Authorize(AuthenticationSchemes = "Google")]
        [HttpGet("google")]
        public IActionResult LoginByGoogle()
        {
            var jwt = new { jwt = getToken().RawData };
            return Ok(JsonConvert.SerializeObject(jwt));
        }






















        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme)
                .GetAwaiter().GetResult();
            return Ok();
        }

        private JwtSecurityToken getToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthConstants.SECRET));
            var handler = new JwtSecurityTokenHandler();
            var tokenDescr = new SecurityTokenDescriptor()
            {
                Issuer = AuthConstants.ISSUER_NAME,
                Audience = AuthConstants.AUDIENCE_NAME,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email,  User.Claims.FirstOrDefault(t=>t.Type==ClaimTypes.Email).Value),
                    new Claim(ClaimTypes.Name, User.Claims.FirstOrDefault(t=>t.Type==ClaimTypes.Name).Value),
                    new Claim(ClaimTypes.NameIdentifier, User.Claims.FirstOrDefault(t=>t.Type==ClaimTypes.NameIdentifier).Value),
                }),
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddMinutes(15),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512)
            };
            IdentityModelEventSource.ShowPII = true;
            var token = handler.CreateJwtSecurityToken(tokenDescr);
            return token;
        }
    }
}