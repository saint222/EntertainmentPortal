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
using EP.SeaBattle.Common.Enums;

namespace EP.SeaBattle.Logic.Handlers
{
    public class AddShotHandler : IRequestHandler<AddShotCommand, Maybe<IEnumerable<Shot>>>
    {
        private readonly SeaBattleDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<AddShotCommand> _validator;
        private readonly ILogger _logger;

        private const int SIZE = 10;
        public AddShotHandler(SeaBattleDbContext context, IMapper mapper, IValidator<AddShotCommand> validator, ILogger<AddShotHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Maybe<IEnumerable<Shot>>> Handle(AddShotCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, ruleSet: "AddShotValidation", cancellationToken: cancellationToken).ConfigureAwait(false);
            if(validationResult.IsValid) 
            {
                var game = await _context.Games.Include(i => i.Player1).Include(i => i.Player2).Include(i => i.Shots)
                    .FirstOrDefaultAsync(f => f.Id == request.GameId).ConfigureAwait(false);
                var player = await _context.Players.FindAsync(request.PlayerId).ConfigureAwait(false);
                //get enemy ships
                var enemyShips = await _context.Ships.Where(w => w.Game.Id == request.GameId && w.Player.Id != request.PlayerId)
                    .Include(i => i.Cells).ToArrayAsync().ConfigureAwait(false);
                var playerShots = game.Shots.Where(s => s.PlayerId == request.PlayerId);
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
                        var enemyShots = game.Shots.Where(s => s.PlayerId != request.PlayerId);
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
                                    count = 0;
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
                        {
                            cell.Status = shot.Status;
                            await _context.Shots.AddAsync(_mapper.Map<ShotDb>(shot)).ConfigureAwait(false);
                            if (cell.Ship.Cells.All(c => c.Status == Common.Enums.CellStatus.Destroyed))
                            {
                                SetForbiddenCellsAround(request.GameId, request.PlayerId, _mapper.Map<Ship>(cell.Ship), _mapper.Map<IEnumerable<Shot>>(playerShots));
                            }
                        }    
                    }

                    //set game finish
                    var shipManager = new ShipsManager(null, null, _mapper.Map<IEnumerable<Ship>>(enemyShips));
                    if (shot.Status == Common.Enums.CellStatus.Destroyed && shipManager.AllShipsDestroyed)
                        game.Status = Common.Enums.GameStatus.Finished;
                    try
                    {
                        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                        var shots = await _context.Shots.Where(s => s.PlayerId == request.PlayerId && s.GameId == request.GameId).ToArrayAsync().ConfigureAwait(false);
                        return Maybe<IEnumerable<Shot>>.From(_mapper.Map<IEnumerable<Shot>>(shots));
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
                _logger.LogInformation(string.Join(", ", validationResult.Errors));
                return Maybe<IEnumerable<Shot>>.None;
            }
        }

        private async void SetForbiddenCellsAround(string gameId, string playerId, Ship ship, IEnumerable<Shot> shots)
        {
            FieldManager fieldManager = new FieldManager(new List<Ship>() { ship }, shots);
            var forbiddenCells = new List<Shot>();
            for (int x = 0; x < SIZE; x++)
            {
                for (int y = 0; y < SIZE; y++)
                {
                    if (fieldManager.Cells[x, y].Status == CellStatus.Forbidden)
                    forbiddenCells.Add(new Shot(playerId, gameId, fieldManager.Cells[x, y].X, fieldManager.Cells[x, y].Y, CellStatus.Forbidden));
                }
            }
            await _context.Shots.AddRangeAsync(_mapper.Map<IEnumerable<ShotDb>>(forbiddenCells)).ConfigureAwait(false);
        }
    }
}
