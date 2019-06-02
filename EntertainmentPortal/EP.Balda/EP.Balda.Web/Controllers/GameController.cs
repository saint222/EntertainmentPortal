using EP.Balda.Logic.Models;
using Microsoft.AspNetCore.Mvc;

namespace EP.Balda.Web.Controllers
{
    [Route("api/[controller]")]
    public class GameController : Controller
    {
        [HttpGet]
        public IActionResult Get([FromBody] Player player1, Player player2, char[] word)
        {
            return Ok();
        }
    }
}