using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EP.WordsMaker.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
		// GET: api/Game
		[HttpGet]// Name = "GetGame")]
		public IEnumerable<string> GetAllGames()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Game/5
        [HttpGet("{id}")]//, Name = "GetGame")]
        public string GetGame(int id)
        {
            return "value";
        }

        // POST: api/Game
        //[HttpPost(Name = "PostGame")]
        //public void PostGame([FromBody] string value)
        //{
        //}

        //// PUT: api/Game/5
        //[HttpPut("{id}", Name = "PutName")]
        //public void PutGame(int id, [FromBody] string value)
        //{

        //}

        // DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void DeleteGame(int id)
        //{
        //}
    }
}
