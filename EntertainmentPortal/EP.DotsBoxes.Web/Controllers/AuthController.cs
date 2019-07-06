using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EP.DotsBoxes.Logic.Models;
using EP.DotsBoxes.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace EP.DotsBoxes.Web.Controllers
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


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserRegister userInfo)
        {
            var user = await _manager.FindByNameAsync(userInfo.UserName);

            if (user == null)
            {
                var newUser = new IdentityUser()
                {
                    UserName = userInfo.UserName,
                    Email = userInfo.Email,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var result = await _manager.CreateAsync(newUser, userInfo.Password);

                if (result.Succeeded)
                {                 
                    var token = await _manager.GenerateEmailConfirmationTokenAsync(newUser);
                    var callbackUrl = Url.Action("ConfirmEmail", "Auth", new { userId = newUser.Id, token = token }, Request.Scheme);
                    var emailService = new EmailService();
                    await emailService.SendEmailAsync(userInfo.Email,
                   "Confirm your account",
                   "Please confirm your account by clicking this link: <a href=\""
                                                   + callbackUrl + "\">link</a>");

                    return Ok();
                }
            }

            return BadRequest($"User {userInfo.UserName} already exists");
        }


        [HttpGet("confirmemail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {           
            var user = await _manager.FindByIdAsync(userId);

            if (user == null)
            {
                return BadRequest("User does not exist");
            }

            var result = await _manager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                return Ok();
            }           
                return BadRequest(result.Errors);          
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserRegister userInfo)
        {
            var user = await _manager.FindByNameAsync(userInfo.UserName);

            if (user == null)
            {
                return BadRequest("User doesn't exist");
            }

            var confirmEmail = await _manager.IsEmailConfirmedAsync(user);

            if (!confirmEmail)
            {
                return BadRequest("You didn't confirm your e-mail");
            }

            var result = await _signInManager.PasswordSignInAsync(userInfo.UserName, userInfo.Password, true, false);            
            return result.Succeeded ? (IActionResult)Ok() : BadRequest();         
        }

        [HttpGet("token")]
        public async Task<IActionResult> GetToken(string userName)
        { 
            var user = await _manager.FindByNameAsync(userName);
            if (user == null)
            {
                return BadRequest("User doesn't exist");
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(DotsBoxesConstants.SECRET));
            var handler = new JwtSecurityTokenHandler();
            var tokenDescr = new SecurityTokenDescriptor()
            {
                Issuer = DotsBoxesConstants.ISSUER_NAME,
                Audience = DotsBoxesConstants.AUDIENCE_NAME,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("sub", user.Id.ToString()),
                    new Claim("name", userName),
                }),
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.Now.AddSeconds(60),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512)
            };

            var token = handler.CreateJwtSecurityToken(tokenDescr);

            return Ok(handler.WriteToken(token));
        }


        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}