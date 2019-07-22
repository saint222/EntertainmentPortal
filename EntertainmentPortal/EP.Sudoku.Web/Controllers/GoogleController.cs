using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EP.Sudoku.Web.Controllers
{
    /// <summary>
    /// (OAuth) Google controller.
    /// </summary>
    [ApiController]
    [Route("api/google")]
    public class GoogleController : Controller
    {
        /// <summary>
        /// Method Index of (OAuth) Google controller (CallbackPath).
        /// </summary>
        [HttpGet("")]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}