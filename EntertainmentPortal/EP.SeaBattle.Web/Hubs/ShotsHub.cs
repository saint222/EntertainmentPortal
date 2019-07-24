using EP.SeaBattle.Data.Context;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using EP.SeaBattle.Logic.Models;
using AutoMapper;
using System.Collections.Generic;

namespace EP.SeaBattle.Web
{
    public class ShotsHub : Hub
    {
        private readonly ILogger<ShotsHub> _logger;
        private readonly SeaBattleDbContext _context;
        private readonly IMapper _mapper;

        public ShotsHub(ILogger<ShotsHub> logger, SeaBattleDbContext seaBattleDbContext, IMapper mapper)
        {
            _logger = logger;
            _context = seaBattleDbContext;
            _mapper = mapper;
        }

        public async Task getShots(string gameId, string playerId)
        {
            await Clients.Caller.SendAsync("receiveShots", sendShots(gameId, playerId));
        }

        private async Task<IEnumerable<Shot>> sendShots(string gameId, string answerPlayerId)
        {
            var shots = await _context.Shots.Where(shot => shot.GameId == gameId && shot.PlayerId == answerPlayerId).ToArrayAsync();
            return _mapper.Map<IEnumerable<Shot>>(shots);
        }
    }

    public interface IShotHub
    {
        Task SendShots();
    }
}
