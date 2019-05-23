using System.Collections.Generic;
using EP.Balda.Models;
using Microsoft.AspNetCore.Mvc;

namespace EP.Balda.Web.Controllers
{
    [Route("api/[controller]")]
    public class WordController : Controller
    {
        [HttpGet]
        public List<Cell> Get()
        {
            Word word = new Word();
            word.Cells = new List<Cell>();
            return word.Cells;
        }
        
        [HttpPost]
        public IActionResult Post([FromBody]Word word)
        {
            return Ok(word);
        }
    }
}
