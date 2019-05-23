using System.Collections.Generic;
using EP.Balda.Logic.Models;
using Microsoft.AspNetCore.Mvc;

namespace EP.Balda.Web.Controllers
{
    [Route("api/[controller]")]
    public class WordController : Controller
    {
        [HttpGet]
        public List<Cell> Get()
        {
            var word = new Word {Cells = new List<Cell>()};
            return word.Cells;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Word word)
        {
            return Ok(word);
        }
    }
}