using EP.Balda.Logic.Models;
using Microsoft.AspNetCore.Mvc;

namespace EP.Balda.Web.Controllers
{
    [Route("api/[controller]")]
    public class CellController : Controller
    {
        [HttpGet]
        public IActionResult Get(int x, int y)
        {
            var cell = new Cell(x, y);
            return Ok(cell.Letter);
        }

        [HttpPost]
        public IActionResult Post([FromBody] int x, int y, char letter)
        {
            var cell = new Cell(x, y) { Letter = letter };
            return Ok(cell);
        }
    }
}