using Microsoft.AspNetCore.Mvc;

namespace EP.Hangman.Web.Controllers
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