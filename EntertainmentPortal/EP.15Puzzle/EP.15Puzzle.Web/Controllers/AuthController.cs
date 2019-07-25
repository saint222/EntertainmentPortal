using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EP._15Puzzle.Logic.Commands;
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

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("test")]
        public IActionResult LoginTest()
        {
            var list = new List<string>();
            foreach (var claim in User.Claims)
            {
                list.Add(claim.Type+':'+claim.Value);
            }
            return Ok(list);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("testuser")]
        public IActionResult LoginTestUser([FromBody] NewDeckCommand newDeck)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            

            return Ok();
        }


    }
}