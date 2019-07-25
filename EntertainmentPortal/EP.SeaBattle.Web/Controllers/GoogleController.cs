using Microsoft.AspNetCore.Mvc;

namespace EP.SeaBattle.Web.Controllers
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
