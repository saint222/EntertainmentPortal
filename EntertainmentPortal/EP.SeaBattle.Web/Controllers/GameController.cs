using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using EP.SeaBattle.Data.Context;
using EP.SeaBattle.Data.Models;
using EP.SeaBattle.Logic.Commands;
using EP.SeaBattle.Logic.Models;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NJsonSchema.Annotations;
using NSwag.Annotations;

namespace EP.SeaBattle.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly SeaBattleDbContext _context;
        private readonly IMapper _mapper;

        public GameController(IMediator mediator, SeaBattleDbContext seaBattleDbContext, IMapper mapper)
        {
            _mediator = mediator;
            _context = seaBattleDbContext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("new")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Game), Description = "Create new game")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Can't create a new game")]
        public async Task<IActionResult> CreateGame(string playerId, string gameId)
        {
            var guidGameId = Guid.NewGuid().ToString();
            HttpContext.Session.SetString("game", guidGameId);
            playerId = HttpContext.Session.GetString("player");
            var player = await _context.Players.FindAsync(playerId).ConfigureAwait(false);
            var game = new GameDb() { Id = guidGameId, Player1 = player, PlayerAllowedToMove = player, Status = Common.Enums.GameStatus.NotReady };
            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            //gameId = guidGameId;
            //playerId = HttpContext.Session.GetString("player");
            //var model = new CreateNewGameCommand() { GameId = gameId, PlayerId = playerId };
            //var result = await _mediator.Send(model);
            //return result.IsSuccess ? Ok(result.Value)
            //    : (IActionResult)Ok();
            return (IActionResult)Ok();
        }
    }
}
