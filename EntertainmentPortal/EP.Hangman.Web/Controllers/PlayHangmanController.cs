using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EP.Hangman.Logic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EP.Hangman.Web;
using MediatR;
using EP.Hangman.Logic.Queries;
using NSwag;
using NSwag.Annotations;

namespace EP.Hangman.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayHangmanController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PlayHangmanController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //GET: api/PlayHangman
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, typeof(HangmanDataResponse), Description = "Cool")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Database is empty")]
        public async Task<IActionResult> GetHangmanAsync()
        {
            var result = await _mediator.Send(new GetHangman());
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }

        //POST: api/PlayHangman
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Created, typeof(HangmanDataResponse), Description = "Cool")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Object didn't create")]
        public async Task<IActionResult> PostHangmanAsync()
        {
            var result = await _mediator.Send(new PostHangman());
            return result != null ? (IActionResult)Ok(result) : BadRequest();
        }

        //PUT: api/PlayHangman/{letter}
        [HttpPut("{letter}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(HangmanDataResponse), Description = "Cool")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Data didn't update")]
        public async Task<IActionResult> CheckLetterAsync(string letter)
        {
            var result = await _mediator.Send(new PutHangman(letter));
            return result != null ? (IActionResult)Ok(result) : BadRequest();
        }

        //PUT: api/PlayHangman
        [HttpPut]
        [SwaggerResponse(HttpStatusCode.OK, typeof(HangmanDataResponse), Description = "Cool")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Data didn't update")]
        public async Task<IActionResult> CheckLetterFromBodyAsync([FromBody]string letter)
        {
            var result = await _mediator.Send(new PutHangman(letter));
            return result != null ? (IActionResult)Ok(result) : BadRequest();
        }
    }
}
