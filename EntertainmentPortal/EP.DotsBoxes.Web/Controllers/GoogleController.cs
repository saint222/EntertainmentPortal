using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EP.DotsBoxes.Web.Controllers
{
    [Route("api/google")]
    [ApiController]
    public class GoogleController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}