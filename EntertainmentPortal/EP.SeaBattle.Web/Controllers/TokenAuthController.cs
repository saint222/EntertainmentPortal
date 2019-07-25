using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NJsonSchema.Annotations;
using NSwag.Annotations;
using System.Collections.Generic;

namespace EP.SeaBattle.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenAuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _manager;
        private readonly ILogger _logger;

        public TokenAuthController(UserManager<IdentityUser> manager, ILogger<TokenAuthController> logger)
        {
            _manager = manager;
            _logger = logger;
        }

        private string GetToken(IdentityUser userData)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SeaBattleConstants.SECRET));
            var handler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = SeaBattleConstants.ISSUER_NAME,
                Audience = SeaBattleConstants.AUDIENCE_NAME,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("sub", userData.Id),
                    new Claim("name", userData.UserName),
                }),
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddSeconds(30),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512)
            };
            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }
        
        [HttpPost("register")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(void), Description = "User was registered")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(IEnumerable<IdentityError>), Description = "User wasn't registered")]
        public async Task<IActionResult> RegisterWithToken([FromBody]AuthData userAuthData)
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
                    return Ok(GetToken(user));

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
        
        [HttpPost("login")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(void), Description = "User was logged in")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "User wasn't logged in")]
        public async Task<IActionResult> LoginWithToken([FromBody]AuthData userAuthData)
        {
            var user = await _manager.FindByEmailAsync(userAuthData.Email);
            if (user != null)
            {
                var status = await _manager.CheckPasswordAsync(user, userAuthData.Password);
                if (status)
                {
                    _logger.LogInformation($"{user.UserName} was logged in");
                    
                    return Ok(GetToken(user));

                }
                else
                {
                    _logger.LogWarning("Login failed. Wrong Password");
                    return BadRequest("Wrong Password");
                }
            }
            else
            {
                _logger.LogWarning("Login failed. User doesn't exist");
                return BadRequest("User doesn't exist");
            }
        }

    }
}