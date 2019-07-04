using Microsoft.AspNetCore.Mvc;

namespace EP.Hangman.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GoogleController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}