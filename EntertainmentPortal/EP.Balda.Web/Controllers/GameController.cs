using EP.Balda.Models;
using Microsoft.AspNetCore.Mvc;

namespace EP.Balda.Web.Controllers
{
    [Route("api/[controller]")]
    public class GameController : Controller
    {
        [HttpGet]
        public IActionResult Get([FromBody]Player player1, Player player2, char[] word)
        {
            Game game = new Game(player1, player2, word);
            return Ok(game);
        }
    }
}
