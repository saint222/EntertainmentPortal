using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using EP.Sudoku.Logic.Models;
using EP.Sudoku.Logic.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using NSwag.Annotations;

namespace EP.Sudoku.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _manager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSenderService _emailSenderSenderService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(UserManager<IdentityUser> manager, SignInManager<IdentityUser> signInManager, 
            IEmailSenderService emailSenderSenderService, ILogger<AuthController> logger)
        {
            _manager = manager;
            _emailSenderSenderService = emailSenderSenderService;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpPost("register")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(void), Description = "User was registered")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(IEnumerable<IdentityError>), Description = "User wasn't registered")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var checkUser = await _manager.FindByNameAsync(user.Name);
            if (checkUser == null)
            {
                var newUser = new IdentityUser()
                {                    
                    UserName = user.Name,
                    Email = user.Email
                };
                var status = await _manager.CreateAsync(newUser, user.Password);
                if (status.Succeeded)
                {
                    _logger.LogInformation($"A new user with the Id '{newUser.Id}' was registered...");
                    await _signInManager.SignInAsync(newUser, false);
                }
                else
                {
                    _logger.LogError($"Registration failed...\n{status.Errors}");
                }

                return status.Succeeded ? (IActionResult)Ok() : BadRequest(status.Errors);
            }
            else
            {
                _logger.LogError("Register failed. User already exists...");
                return BadRequest("User already exists...");
            }

        }

        [HttpPost("login")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(void), Description = "User was logged in")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "User wasn't logged in")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            var result = await _signInManager.PasswordSignInAsync(user.Name, user.Password, true, false);
            if (result.Succeeded)
            {
                _logger.LogInformation($"The user with the email '{user.Email}' was signed in...");
            }
            else
            {
                _logger.LogWarning($"The process of signing in with the email '{user.Email}' got failed...");
            }
            return result.Succeeded ? (IActionResult)Ok() : BadRequest();
        }

        [SwaggerResponse(HttpStatusCode.OK, typeof(void), Description = "User was logged out")]
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            _logger.LogInformation($"User '{_signInManager.Context.User.Identity.Name}' was logged out...");
            return Ok();
        }

        [HttpPost("registerByEmail")]
        public async Task<IActionResult> RegisterByEmail([FromBody]User user)
        {
            var checkUser = await _manager.FindByNameAsync(user.Name);
            if (checkUser == null)
            {
                var newUser = new IdentityUser()
                {
                    UserName = user.Name,
                    Email = user.Email
                };
                var status = await _manager.CreateAsync(newUser, user.Password);
                if (status.Succeeded)
                {
                    _logger.LogInformation($"A new user with the Id '{newUser.Id}' was registered...");
                    await _signInManager.SignInAsync(newUser, false);

                    var code = await _manager.GenerateEmailConfirmationTokenAsync(newUser);

                    var callbackUrl = Url.Page(
                        "/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = newUser.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSenderSenderService.SendEmailAsync(user.Email, "Confirm your email",
                        $"Please confirm your account by <a href = '{HtmlEncoder.Default.Encode(callbackUrl)}' > click here </a>.");

                }
                else
                {
                    _logger.LogError($"Registration failed...\n{status.Errors}");
                }

                return status.Succeeded ? (IActionResult)Ok() : BadRequest(status.Errors);
            }
            else
            {
                _logger.LogError("Register failed. User already exists...");
                return BadRequest("User already exists...");
            }
        }

        [HttpGet("token")]
        public IActionResult GetToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SudokuConstants.SECRET));
            var handler = new JwtSecurityTokenHandler();
            var tokenDescr = new SecurityTokenDescriptor()
            {
                Issuer = SudokuConstants.ISSUER_NAME,
                Audience = SudokuConstants.AUDIENCE_NAME,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, $"{User.Identity.Name}")                    
                }),
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddSeconds(15),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512)
            };

            var token = handler.CreateJwtSecurityToken(tokenDescr);

            return Ok(handler.WriteToken(token));
        }
    }
}