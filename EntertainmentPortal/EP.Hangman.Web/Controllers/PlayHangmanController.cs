using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EP.Hangman.Web;

namespace EP.Hangman.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayHangmanController : ControllerBase
    {
        //private PlayHangman _game = new PlayHangman(); 

        // GET: api/PlayHangman
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/PlayHangman/5
        //[HttpGet("{id}", Name = "Get")]
        //public ActionResult<string> Get(string id)
        //{


        //    return Ok($"Status: {_game.PlayGame(id)}. Gueesed word: {_game.CorrectLetters}. Incorect letters: {_game.WrongLetters}. Attemts left: {_game.UserAttempts}.");
        //}

        // POST: api/PlayHangman
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/PlayHangman/5
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
