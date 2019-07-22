using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.SeaBattle.Data.Context;
using EP.SeaBattle.Data.Models;
using EP.SeaBattle.Logic.Commands;
using EP.SeaBattle.Logic.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using EP.SeaBattle.Common.Enums;
using System.Collections.Generic;

namespace EP.SeaBattle.Logic.Handlers
{
    public class AddShotHandler : IRequestHandler<AddShotCommand, Result<Shot>>
    {
        private readonly SeaBattleDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<AddShotCommand> _validator;
        private readonly ILogger _logger;
        public AddShotHandler(SeaBattleDbContext context, IMapper mapper, IValidator<AddShotCommand> validator, ILogger<AddShotHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<Shot>> Handle(AddShotCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, ruleSet: "AddShotValidation", cancellationToken: cancellationToken);
            Result<Shot> result;
            if (validationResult.IsValid)
            {
                ShotDb shotDb = new ShotDb
                {
                    X = request.X,
                    Y = request.Y,
                    PlayerId = request.PlayerId
                   
                    
                };

                _context.Shots.Add(shotDb);

                PlayerDb playerDb = await _context.Players.FindAsync(request.PlayerId).ConfigureAwait(false);

                GameDb gameDb = await _context.Games.Where(g => g.Players.Where(p => p.Id == playerDb.Id).Count() > 0).FirstOrDefaultAsync().ConfigureAwait(false);

                Player enemyPlayerDb = _mapper.Map<Player>(await _context.Players
                                                                       .Include(p => p.Ships)
                                                                       .ThenInclude(s => s.Cells)
                                                                       .FirstOrDefaultAsync(p => p.GameId == gameDb.Id && p.Id != request.PlayerId)
                                                                       .ConfigureAwait(false));
                ShotsManager shotsManager = new ShotsManager(enemyPlayerDb);


                
                Ship ship = shotsManager.TryShoot(request.X, request.Y);
                if (ship != null)
                {
                    ShipDb shipDb = await _context.Ships.FindAsync(ship.Id).ConfigureAwait(false);
                    shipDb.Rank = ship.Rank;
                    shipDb.PlayerId = ship.PlayerId;
                    shipDb.Cells = _mapper.Map<ICollection<CellDb>>(ship.Cells);

                    if (shotsManager.isFinishedGame)
                    {
                        gameDb.Finish = true;
                        gameDb.PlayerAllowedToMove = null;
                        gameDb.Winner = playerDb.NickName;
                        gameDb.Loser = enemyPlayerDb.NickName;
                        playerDb.GameId = null;
                        enemyPlayerDb.GameId = null;
                        IEnumerable<ShotDb> shotsDb = _context.Shots.Where(s => s.PlayerId == playerDb.Id || s.PlayerId == enemyPlayerDb.Id);
                        _context.RemoveRange(shotsDb);
                        IEnumerable<ShipDb> shipsDb = _context.Ships.Where(s => s.PlayerId == playerDb.Id || s.PlayerId == enemyPlayerDb.Id);
                        _context.RemoveRange(shipsDb);

                    }

                    if (ship.IsAlive)
                    {
                        shotDb.Status = ShotStatus.Hurt;
                        result = Result.Ok(_mapper.Map<Shot>(shotDb));
                    }
                    else
                    {
                        shotDb.Status = ShotStatus.Killed;

                        result = Result.Ok(_mapper.Map<Shot>(shotDb));
                    }
                }
                else
                {
                    _logger.LogInformation($"Missed the ship with cordinates X{request.X}, Y{request.Y}");
                    shotDb.Status = ShotStatus.Missed;
                    gameDb.PlayerAllowedToMove = enemyPlayerDb.Id;
                    result = Result.Ok(_mapper.Map<Shot>(shotDb));
                }
            }
            else
            {
                _logger.LogWarning(string.Join(", ", validationResult.Errors));
                result = Result.Fail<Shot>(validationResult.Errors.First().ErrorMessage);
            }
            try
            {
                if (result.IsSuccess)
                {
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                }

            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message);
                result = Result.Fail<Shot>("Cannot add shot");
            }
            return result;
        }
    }
}
