using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EP.Hangman.Web.Controllers
{
    [ApiController]
    [Route("api/facebook")]
    public class FacebookController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
