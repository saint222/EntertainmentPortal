using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EP.Hangman.Security.Models;
using IdentityModel;
using IdentityServer4.Quickstart.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace EP.Hangman.Security.Quickstart.Account
{
    [Route("[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _manager;

        public RegistrationController (UserManager<ApplicationUser> manager)
        {
            _manager = manager;
        }
        
        // POST
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterInputModel userAuthData)
        {
            var user = await _manager.FindByNameAsync(userAuthData.UserName);
            if (user == null)
            {
                var newUser = new ApplicationUser()
                {
                    UserName = userAuthData.UserName
                };

                var status = await _manager.CreateAsync(newUser, userAuthData.Password);
                if (status.Succeeded)
                {
                    status = await _manager.AddClaimsAsync(newUser, new Claim[]
                    {
                        new Claim(JwtClaimTypes.Name, userAuthData.UserName),
                        //new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                        //new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    });
                    if (!status.Succeeded)
                    {
                        throw new Exception(status.Errors.First().Description);
                    }
                    return Ok();
                }
                else
                {
                    return BadRequest(status.Errors);
                }
            }
            else
            {
                return Ok();
            }
        }
    }
}