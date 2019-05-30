using EP.Balda.Logic.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EP.Balda.Web.Controllers
{
    [ApiController]
    public class CellController : Controller
    {
        [HttpGet]
        public IActionResult GetCell(int x, int y)
        {
            var cell = new Cell(x, y);
            return Ok(cell.Letter);
        }

        [HttpPost]
        public IActionResult PostCell([FromBody] int x, int y, char letter)
        {
            var cell = new Cell(x, y) { Letter = letter };
            return Ok(cell);
        }
    }
}