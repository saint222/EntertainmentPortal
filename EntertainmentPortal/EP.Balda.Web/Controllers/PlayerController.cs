using EP.Balda.Models;
using Microsoft.AspNetCore.Mvc;

namespace EP.Balda.Web.Controllers
{
    [Route("api/[controller]")]
    public class PlayerController : Controller
    {
        [HttpGet]
        public IActionResult Get(Player player)
        {
            return Ok(player);
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            Player player = new Player();
            player.Id = id;
            return Ok(player);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Player player)
        {
            return Ok(player);
        }

        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody]string name)
        {
            Player player = new Player();
            player.Id = id;
            player.Name = name;
            return Ok();
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            Player player = new Player();
            player.Id = id;
            return Ok();
        }
    }
}
