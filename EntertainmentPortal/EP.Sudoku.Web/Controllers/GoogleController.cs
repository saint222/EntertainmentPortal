using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EP.Sudoku.Web.Controllers
{
    [ApiController]
    [Route("api/google")]
    public class GoogleController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return Ok();
        }


    }
}