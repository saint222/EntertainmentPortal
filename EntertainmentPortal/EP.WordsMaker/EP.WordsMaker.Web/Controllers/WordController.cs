using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EP.WordsMaker.Logic.Commands;
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
    public class WordController : ControllerBase
    {
	    private readonly IMediator _mediator;

		public WordController(IMediator mediator)
	    {
		    _mediator = mediator;
	    }

		// GET: api/Action
		[SwaggerResponse(HttpStatusCode.OK, typeof(Word), Description = "Success. Get all words")]
		[SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "The words does not exist")]
		[HttpGet("AllWords/{Id}")]
        public async Task<IActionResult> GetAllWords(string Id)
        {
			var result = await _mediator.Send(new GetAllWordsCommand() { GameId = Id });
			return result.IsSuccess ? Ok(result.Value) : (IActionResult)NotFound(result.Error);
		}

        // GET: api/Action
        [HttpGet("KeyWord")]
        public async Task<IActionResult> GetKeyWord()
        {
	        return null;
        }

        [HttpPost("SubmitWord")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Word), Description = "Success. Word added")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Еhe word does not exist")]
		public async Task<IActionResult> AddNewWordAsyncNewWord([FromBody] AddNewWordCommand model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var result = await _mediator.Send(model);
			return result.IsFailure ?
				(IActionResult)BadRequest(result.Error)
				: Ok(result.Value);
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
