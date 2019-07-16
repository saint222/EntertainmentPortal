using Microsoft.AspNetCore.Mvc;

namespace EP.TicTacToe.Web.Controllers
{
    [ApiController]
    [Route("api/google")]
    public class GoogleController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}