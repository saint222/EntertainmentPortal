using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EP._15Puzzle.Web.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace EP._15Puzzle.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeckController : ControllerBase
    {
        Deck deck = new Deck(4);
        // GET: api/Deck
        [HttpGet]
        public IEnumerable<string> Get()
        {
            string[] result = new string[16];
            for (int i = 0; i <= 15; i++)
            {
                result[i] = $"{deck.GetTiles[i].Num} : x={deck.GetTiles[i].PosX}, y={deck.GetTiles[i].PosY}";
            }
            return  result ;
        }

        // GET: api/Deck/5
        [HttpGet("{size}", Name = "Get")]
        public IActionResult Get(int size)
        {
            deck = new Deck(size);
            return new ContentResult()
            {
                Content = deck.ToHTML(),
                ContentType = "text/html"
            };
        }

        // POST: api/Deck
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Deck/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
