using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.TicTacToe.Data.Context;
using EP.TicTacToe.Data.Models;
using EP.TicTacToe.Logic.Commands;
using EP.TicTacToe.Logic.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EP.TicTacToe.Logic.Handlers
{
    public class AddNewGameHandler : IRequestHandler<AddNewGameCommand, Result<Game>>
    {
        private readonly TicTacDbContext _context;
        private readonly IMapper _mapper;

        public AddNewGameHandler(TicTacDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<Game>> Handle(AddNewGameCommand request,
                                               CancellationToken cancellationToken)
        {
            //  Check for the existence of players in the database.
            var playerOne = await (_context.Players
                .Where(p => p.Id == request.PlayerOne)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken));
            if (playerOne == null)
                return Result.Fail<Game>(
                    $"There is no player's id {request.PlayerOne} in database.");

            var playerTwo = await (_context.Players
                .Where(p => p.Id == request.PlayerTwo)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken));
            if (playerTwo == null)
                return Result.Fail<Game>(
                    $"There is no player's id {request.PlayerTwo} in database.");


            //  Creating new clean game.
            var gameDb = new GameDb();
            await _context.Games.AddAsync(gameDb, cancellationToken);

            // Creating new map with received parameters
            var mapDb = new MapDb
                {Size = request.MapSize, WinningChain = request.MapWinningChain};
            await _context.Maps.AddAsync(mapDb, cancellationToken);

            var cells = CreateCellsForMap(mapDb);
            await _context.AddRangeAsync(cells, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            var game = await _context.Games
                .Where(g => g.Id == gameDb.Id)
                .SingleOrDefaultAsync(cancellationToken);
            try
            {
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Ok(_mapper.Map<Game>(game));
            }
            catch (DbUpdateException ex)
            {
                return Result.Fail<Game>(ex.Message);
            }
        }

        /// <summary>
        ///     The method initializes add cells to DB that represents the game map.
        /// </summary>
        /// <param name="mapDb">Map database projection</param>
        public static IEnumerable<CellDb> CreateCellsForMap(MapDb mapDb)
        {
            var cells = new List<CellDb>();

            for (var i = 0; i < mapDb.Size; i++)     // lines
                for (var j = 0; j < mapDb.Size; j++) // columns
                {
                    var cell = new CellDb
                    {
                        X = i,
                        Y = j,
                        TicTac = 0
                    };

                    cells.Add(cell);
                }

            return cells;
        }
    }
}