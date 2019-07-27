using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using EP.SeaBattle.Data.Context;
using EP.SeaBattle.Data.Models;
using EP.SeaBattle.Logic.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> CreateGame()
        {
            var guidGameId = Guid.NewGuid().ToString();
            HttpContext.Session.SetString("game", guidGameId);
            string playerId;
            if (HttpContext.Session.Keys.Contains("player"))
            {
                playerId = HttpContext.Session.GetString("player");
            }
            else
            {
                return (IActionResult)BadRequest("No playerId in session");
            }

            var player = await _context.Players.FindAsync(playerId).ConfigureAwait(false);
            var game = new GameDb() { Id = guidGameId, Player1 = player, PlayerAllowedToMove = player, Status = Common.Enums.GameStatus.NotReady };
            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();
            return (IActionResult)Ok();
        }
    }
}
