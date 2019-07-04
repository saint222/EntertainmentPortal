using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EP._15Puzzle.Logic;
using EP._15Puzzle.Logic.Commands;
using EP._15Puzzle.Logic.Models;
using EP._15Puzzle.Logic.Queries;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;

namespace EP._15Puzzle.Web.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/user
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, typeof(User), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> Get()
        {
            Result<User> result = new Result<User>();
            if (HttpContext.Request.Cookies.ContainsKey("id"))
            {
                int id = int.Parse(HttpContext.Request.Cookies["id"]);
                result = await _mediator.Send(new GetUserQuery(id));
            }
            if (result.IsSuccess)
            {
                return (IActionResult)Ok(result.Value);
            }
            return NotFound("User isn't found");
        }

        // POST: api/user
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, typeof(User), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            var result = await _mediator.Send(new NewUserCommand(user.Name, user.Country));
            if (result.IsSuccess)
            {
                HttpContext.Response.Cookies.Append("id", result.Value.Id.ToString());
                return (IActionResult)Created(result.Value.Id.ToString(), result.Value);
            }
            return BadRequest(result.Error);
            
        }

        

        // PUT: api/user
        [HttpPut]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Deck), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> Put([FromBody][NotNull] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Result<User> result = new Result<User>();
            if (HttpContext.Request.Cookies.ContainsKey("id"))
            {
                int id = int.Parse(HttpContext.Request.Cookies["id"]);
                result = await _mediator.Send(new AddInfoAboutUserCommand(id, user.Name, user.Country));
            }
            else
            {
                result = await _mediator.Send(new AddInfoAboutUserCommand(user.Id, user.Name, user.Country));
            }
            
            if (result.IsSuccess)
            {
                return (IActionResult)Ok(result.Value);
            }
            return BadRequest(result.Error);

        }

        // DELETE: api/user/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        
        
    }
    
}
