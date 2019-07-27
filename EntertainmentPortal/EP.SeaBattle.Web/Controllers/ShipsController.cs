using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EP.SeaBattle.Logic.Commands;
using EP.SeaBattle.Logic.Queries;
using EP.SeaBattle.Logic.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using FluentValidation.AspNetCore;
using System.ComponentModel;
using EP.SeaBattle.Data.Context;
using AutoMapper;
using EP.SeaBattle.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EP.SeaBattle.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly SeaBattleDbContext _context;

        public ShipsController(IMediator mediator, SeaBattleDbContext seaBattleDbContext, IMapper mapper)
        {
            _mediator = mediator;
            _context = seaBattleDbContext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getall")]
        [Description("Get all ships")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Ship>), Description = "Get player ships collection")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Can't show player ships")]
        public async Task<IActionResult> GetShipsAsync(string gameId, string playerId)
        {
            var result = await _mediator.Send(new GetShipsQuery() { GameId = gameId, PlayerId = playerId });
            return result.HasValue ? Ok(result.Value)
                : (IActionResult)Ok();

        }

        [HttpPost]
        [Route("add")]
        [Description("Add ship")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Ship>), Description = "Add ship to player collection")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Can't add ship")]
        public async Task<IActionResult> AddShipAsync([FromBody, CustomizeValidator(RuleSet = "AddShipPreValidation")] AddNewShipCommand model)
        {
            if (!HttpContext.Session.Keys.Contains("player"))
            {
                var playerId = Guid.NewGuid().ToString();
                var player = new Player() { Id = playerId, NickName = "Name " + playerId };
                await _context.Players.AddAsync(_mapper.Map<PlayerDb>(player));
                HttpContext.Session.SetString("player", playerId);

                if (!HttpContext.Session.Keys.Contains("game"))
                {
                    var gameId = Guid.NewGuid().ToString();
                    var game = new Game() { Id = gameId, Player1 = player, Status = Common.Enums.GameStatus.NotReady, PlayerAllowedToMove = player };
                    await _context.Games.AddAsync(_mapper.Map<GameDb>(game));
                    HttpContext.Session.SetString("game", gameId);
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.Message);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            model.GameId = HttpContext.Session.GetString("game");
            model.PlayerId = HttpContext.Session.GetString("player");
            var result = await _mediator.Send(model);
            return result.HasValue ? Ok(result.Value)
                :(IActionResult) Ok();
        }

        [HttpDelete]
        [Route("delete")]
        [Description("Delete ship")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Ship>), Description = "Delete ship from collection")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Can't delete ship")]
        public async Task<IActionResult> DeleteShipAsync([CustomizeValidator(RuleSet = "DeleteShipPreValidation")]byte x, byte y)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var model = new DeleteShipCommand() { X = x, Y = y };
            model.PlayerId = HttpContext.Session.GetString("player");
            model.GameId = HttpContext.Session.GetString("game");
            var result = await _mediator.Send(model);
            return result.HasValue ? Ok(result.Value)
                : (IActionResult)Ok();
        }
    }
}
