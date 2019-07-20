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
                                                .Include(p => p.Game)
                                                .FirstOrDefaultAsync(p => p.Id == request.PlayerId).ConfigureAwait(false);
            GameDb gamedb = await _context.Games.FirstOrDefaultAsync(g => g.Id == playerDb.Game.Id).ConfigureAwait(false);
            if (gamedb == null)
            {
                _logger.LogWarning($"Game with id {playerDb.Game.Id} not found");
                return Result.Fail<Game>("Game not found");
            }

            gamedb.Finish = true;
            gamedb.PlayerAllowedToMove = null;
            playerDb.GameId = null;
            gamedb.EnemySearch = false;
            PlayerDb enemyPlayerDb = gamedb.Players.FirstOrDefault(p => p.Id != request.PlayerId);
            if (enemyPlayerDb == null)
            {
                _context.Games.Remove(gamedb);
                IEnumerable<ShipDb> shipsDb = _context.Ships.Where(s => s.PlayerId == playerDb.Id);
                _context.Ships.RemoveRange(shipsDb);
            }
            else
            {
                gamedb.Winner = enemyPlayerDb.NickName;
                gamedb.Loser = playerDb.NickName;
                enemyPlayerDb.GameId = null;
                IEnumerable<ShotDb> shotsDb = _context.Shots.Where(s => s.PlayerId == playerDb.Id || s.PlayerId == enemyPlayerDb.Id);
                _context.Shots.RemoveRange(shotsDb);
                IEnumerable<ShipDb> shipsDb = _context.Ships.Where(s => s.PlayerId == playerDb.Id || s.PlayerId == enemyPlayerDb.Id);
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


