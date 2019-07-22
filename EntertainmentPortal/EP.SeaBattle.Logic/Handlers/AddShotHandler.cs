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
                var game = await _context.Games.Include(i => i.Player1).Include(i => i.Player2)
                    .FirstOrDefaultAsync(f => f.Id == request.GameId).ConfigureAwait(false);
                var player = await _context.Players.FindAsync(request.PlayerId).ConfigureAwait(false);
                //get enemy ships
                var enemyShips = await _context.Ships.Where(w => w.Game.Id == request.GameId && w.Player.Id != request.PlayerId)
                    .Include(i => i.Cells).ToArrayAsync().ConfigureAwait(false); 
                var playerShots = await _context.Shots.Where(w => w.GameId == request.GameId && w.PlayerId == request.PlayerId)
                    .ToArrayAsync().ConfigureAwait(false);
                var shotManager = new ShotManager(_mapper.Map<Game>(game), _mapper.Map<Player>(player), 
                    _mapper.Map<IEnumerable<Ship>>(enemyShips), _mapper.Map<IEnumerable<Shot>>(playerShots));

                if(shotManager.TryShoot(request.X, request.Y))
                {
                    //add shot
                    var shot = shotManager.Shoot(request.X, request.Y);
                    
                    if (shot.Status == Common.Enums.CellStatus.ShootWithoutHit)
                    {
                        await _context.AddAsync(_mapper.Map<ShotDb>(shot)).ConfigureAwait(false);
                        //AI do shots
                        game.PlayerAllowedToMove = game.Player2;
                        var playerShips = await _context.Ships.Where(ship => ship.Game.Id == request.GameId && ship.Player.Id == request.PlayerId)
                            .Include(i => i.Cells).ToArrayAsync().ConfigureAwait(false);
                        var enemyShots = await _context.Shots.Where(s => s.GameId == request.GameId && s.PlayerId != request.PlayerId)
                            .ToArrayAsync().ConfigureAwait(false);
                        AIManager aiManager = new AIManager(_mapper.Map<Game>(game), _mapper.Map<Player>(game.Player2),
                            _mapper.Map<IEnumerable<Ship>>(playerShips), _mapper.Map<IEnumerable<Shot>>(enemyShots));
                        Shot outerShot = new Shot();
                        bool flag = true;
                        int count = 0;
                        while(flag && count < 1000)
                        {
                            if (aiManager.TryShoot(out outerShot))
                            {
                                if (outerShot.Status == Common.Enums.CellStatus.ShootWithoutHit)
                                {
                                    flag = false;
                                }
                                else
                                {
                                    var playerCell = playerShips.SelectMany(s => s.Cells)
                                        .FirstOrDefault(c => c.X == outerShot.X && c.Y == outerShot.Y);
                                    if (playerCell != null)
                                        playerCell.Status = outerShot.Status;
                                }
                                await _context.Shots.AddAsync(_mapper.Map<ShotDb>(outerShot));
                            }
                            count++;
                        }
                        game.PlayerAllowedToMove = game.Player1;
                    }
                    else
                    {
                        //update cell
                        var cell = enemyShips.SelectMany(s => s.Cells).FirstOrDefault(f => f.X == request.X && f.Y == request.Y);
                        if (cell != null)
                            cell.Status = shot.Status;
                    }

                    //set game finish
                    var shipManager = new ShipsManager(null, null, _mapper.Map<IEnumerable<Ship>>(enemyShips));
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
