using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EP.SeaBattle.Data.Context;
using EP.SeaBattle.Data.Models;
using EP.SeaBattle.Logic.Commands;
using EP.SeaBattle.Logic.Models;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace EP.SeaBattle.Logic.Handlers
{
    public class AddShotHandler : IRequestHandler<AddShotCommand, Maybe<IEnumerable<Shot>>>
    {
        private readonly SeaBattleDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<AddNewShipCommand> _validator;
        private readonly ILogger _logger;

        public AddShotHandler(SeaBattleDbContext context, IMapper mapper, IValidator<AddNewShipCommand> validator, ILogger<AddNewShipHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Maybe<IEnumerable<Shot>>> Handle(AddShotCommand request, CancellationToken cancellationToken)
        {
            //TODO Add shot validation
            if(true) 
            {
                var game = await _context.Games.FindAsync(request.GameId).ConfigureAwait(false);
                var player = await _context.Players.FindAsync(request.PlayerId).ConfigureAwait(false);
                //TODO Async?
                var ships = _context.Ships.Where(w => w.Game.Id == request.GameId && w.Player.Id != request.PlayerId).Include(i => i.Cells); //get enemy ships
                var shots = _context.Shots.Where(w => w.GameId == request.GameId && w.PlayerId == request.PlayerId);
                var shotManager = new ShotManager(_mapper.Map<Game>(game), _mapper.Map<Player>(player), 
                    _mapper.Map<IEnumerable<Ship>>(ships), _mapper.Map<IEnumerable<Shot>>(shots));

                if(shotManager.TryShoot(request.X, request.Y))
                {
                    //add shot
                    var shot = shotManager.Shoot(request.X, request.Y);
                    
                    if (shot.Status == Common.Enums.CellStatus.ShootWithoutHit)
                    {
                        await _context.AddAsync(_mapper.Map<ShotDb>(shot)).ConfigureAwait(false);
                    }
                    else
                    {
                        //update cell
                        var cell = ships.SelectMany(s => s.Cells).FirstOrDefault(f => f.X == request.X && f.Y == request.Y);
                        if (cell != null)
                            cell.Status = shot.Status;
                    }

                    //TODO change player allow to move
                    //if (shot.Status != Common.Enums.CellStatus.Destroyed)
                    //{
                    //    if (game.Player1 == player)
                    //        game.PlayerAllowedToMove = game.Player2;
                    //    else
                    //        game.PlayerAllowedToMove = game.Player1;
                    //}
                    //set game finish
                    var shipManager = new ShipsManager(null, null, _mapper.Map<IEnumerable<Ship>>(ships));
                    if (shot.Status == Common.Enums.CellStatus.Destroyed && shipManager.AllShipsDestroyed)
                        game.Status = Common.Enums.GameStatus.Finished;
                    try
                    {
                        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                        return Maybe<IEnumerable<Shot>>.From(_mapper.Map<IEnumerable<Shot>>(shotManager.Shots));
                    }
                    catch (DbUpdateException ex)
                    {
                        _logger.LogError(ex.Message);
                        return Maybe<IEnumerable<Shot>>.None;
                    }
                }
                else
                {
                    _logger.LogInformation($"Shot was not added" +
                        $"Shot info X: {request.X} Y: {request.Y}, Orientation {request.PlayerId}, Rank {request.GameId}, ");
                    return Maybe<IEnumerable<Shot>>.From(_mapper.Map<IEnumerable<Shot>>(shotManager.Shots));
                }
            }
            else
            {
                //_logger.LogInformation(string.Join(", ", validationResult.Errors));
                //return Maybe<IEnumerable<Ship>>.None;
            }
        }
    }
}
