using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EP.Sudoku.Web.Controllers
{
    /// <summary>
    /// (OAuth) Facebook controller.
    /// </summary>
    [Route("api/signInFacebook")]
    [ApiController]
    public class FacebookController : ControllerBase
    {
        /// <summary>
        /// Method Index of (OAuth) Facebook controller (CallbackPath).
        /// </summary>
        [HttpGet("")]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}