using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EP.SeaBattle.Data.Context;
using EP.SeaBattle.Logic.Commands;
using EP.SeaBattle.Logic.Models;
using EP.SeaBattle.Data.Models;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System;
using System.Collections.Generic;

namespace EP.SeaBattle.Logic.Handlers
{
    public class FinishGameHandler : IRequestHandler<FinishGameCommand, Result<Game>>
    {
        private readonly SeaBattleDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public FinishGameHandler(SeaBattleDbContext context, IMapper mapper, ILogger<FinishGameHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<Game>> Handle(FinishGameCommand request, CancellationToken cancellationToken)
        {
            PlayerDb playerDb = await _context.Players
                                                .FirstOrDefaultAsync(p => p.UserId == request.UserId).ConfigureAwait(false);

            GameDb gamedb = await _context.Games.FirstOrDefaultAsync(g => g.Id == playerDb.GameId).ConfigureAwait(false);
            if (gamedb == null)
            {
                _logger.LogWarning($"Game with id {playerDb.GameId} not found");
                return Result.Fail<Game>("Game not found");
            }

            gamedb.Finish = true;
            gamedb.PlayerAllowedToMove = null;
            gamedb.EnemySearch = false;
            playerDb.GameId = null;
            PlayerDb enemyPlayerDb = gamedb.Players.FirstOrDefault(p => p.Id != playerDb.Id);
            if (enemyPlayerDb == null)
            {
                _context.Games.Remove(gamedb);
                IEnumerable<ShipDb> shipsDb = await (_context.Ships.Where(s => s.PlayerId == playerDb.Id)).ToArrayAsync().ConfigureAwait(false);
                _context.Ships.RemoveRange(shipsDb);
            }
            else
            {
                gamedb.Winner = enemyPlayerDb.NickName;
                gamedb.Loser = playerDb.NickName;
                enemyPlayerDb.GameId = null;
                IEnumerable<ShotDb> shotsDb = await _context.Shots.Where(s => ( s.PlayerId == playerDb.Id || s.PlayerId == enemyPlayerDb.Id)).ToArrayAsync().ConfigureAwait(false);
                _context.Shots.RemoveRange(shotsDb);
                IEnumerable<ShipDb> shipsDb = await _context.Ships.Where(s => (s.PlayerId == playerDb.Id || s.PlayerId == enemyPlayerDb.Id)).ToArrayAsync().ConfigureAwait(false);
                _context.Ships.RemoveRange(shipsDb);
            }


            try
            {
                await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                _logger.LogInformation($"Player {gamedb.Finish} changed");
                return Result.Ok<Game>(_mapper.Map<Game>(gamedb));
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message);
                return Result.Fail<Game>("Cannot update game");
            }
        }
    }
}


