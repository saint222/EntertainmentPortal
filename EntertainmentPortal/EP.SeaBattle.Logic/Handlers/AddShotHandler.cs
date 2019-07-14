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
            var validationResult = await _validator.ValidateAsync(request, ruleSet: "AddPlayerValidation", cancellationToken: cancellationToken);
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

                GameDb gameDb = await _context.Games.FirstOrDefaultAsync(g => g.Players.Where(p => p.Id == request.PlayerId) != null).ConfigureAwait(false);

                Player enemyPlayer = _mapper.Map<Player>(await _context.Players
                                                                       .Include(p => p.Ships)
                                                                       .ThenInclude(s => s.Cells)
                                                                       .FirstOrDefaultAsync(p => p.GameId == gameDb.Id && p.Id != request.PlayerId)
                                                                       .ConfigureAwait(false));
                ShotsManager shotsManager = new ShotsManager(enemyPlayer);


                
                Ship ship = shotsManager.TryShoot(request.X, request.Y);
                if (ship != null)
                {
                    _context.Update(_mapper.Map<ShipDb>(ship));
                    if (shotsManager.isFinishedGame)
                    {
                        gameDb.Finish = true;
                        gameDb.PlayerAllowedToMove = null;
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
                    gameDb.PlayerAllowedToMove = enemyPlayer.Id;
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
                await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
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
