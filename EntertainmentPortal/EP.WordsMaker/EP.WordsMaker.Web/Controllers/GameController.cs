using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EP.WordsMaker.Logic.Models;
using EP.WordsMaker.Logic.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace EP.WordsMaker.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
	    private readonly IMediator _mediator;

	    public GameController(IMediator mediator)
	    {
		    _mediator = mediator;
	    }

		[HttpGet]
	    [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Player>), Description = "Success")]
	    [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Geme collection is empty")]
	    public async Task<IActionResult> GetAllGamesAsync()
	    {
		    var result = await _mediator.Send(new GetAllGames());
		    return result.HasValue ? (IActionResult)Ok(result.Value) : NotFound();
	    }

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
