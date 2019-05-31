using EP.Balda.Logic.Models;
using Microsoft.AspNetCore.Mvc;

namespace EP.Balda.Web.Controllers
{
    [Route("api/[controller]")]
    public class MapController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            var field = new Map(5);
            return Ok(field);
        }

        [HttpPut]
        public IActionResult Put(char letter, [FromBody] int x, int y)
        {
            var field = new Map(5);
            return Ok();
        }
    }
}