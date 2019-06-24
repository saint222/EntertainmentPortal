using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EP.WordsMaker.Logic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EP.WordsMaker.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordController : ControllerBase
    {
        // GET: api/Action
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllWords(int id)
        {
	        return null;
        }

        // GET: api/Action
        [HttpGet]
        public async Task<IActionResult> GetKeyWord()
        {
	        return null;
        }

		//// GET: api/Action/5
		//[HttpGet("{id}")]
  //      public string GetWord(int id)
  //      {
  //          return "value";
  //      }

  //      // POST: api/Action
  //      [HttpPost]
  //      public void Action([FromBody] string value)
  //      {
  //      }

        //// PUT: api/Action/5
        //[HttpPut("{id}")]
        //public void Action(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void DeleteAction(int id)
        //{
        //}
    }
}
