using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Bogus;
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
            //  Creating new clean game.
            var gameDb = new GameDb();
            await _context.Games.AddAsync(gameDb, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            // Creating fake haunters if HaunterDb is empty.
            if (!_context.Haunters.Any()) Seed(_context, 100);

            // Creating two new players in the game.
            await CheckPlayerIdForExistenceAsync(_context, request.PlayerOne,
                cancellationToken);
            var firstPlayerDb = new FirstPlayerDb {HaunterId = request.PlayerOne};
            await _context.FirstPlayers.AddAsync(firstPlayerDb, cancellationToken);

            await CheckPlayerIdForExistenceAsync(_context, request.PlayerTwo,
                cancellationToken);
            var secondPlayerDb = new SecondPlayerDb {HaunterId = request.PlayerTwo};
            await _context.SecondPlayers.AddAsync(secondPlayerDb, cancellationToken);

            var playersDb = new PlayerDb
            {
                GameId = gameDb.Id, FirstPlayerId = firstPlayerDb.Id,
                SecondPlayerId = secondPlayerDb.Id
            };
            await _context.Players.AddAsync(playersDb, cancellationToken);

            // Creating new StepResult with received parameters
            var stateResultDb = new StepResultDb
            {
                NextPlayerId = Convert.ToInt32(request.PlayerOne),
                Status = Data.Models.State.Continuation,
                GameId = gameDb.Id
            };
            await _context.StepResults.AddAsync(stateResultDb, cancellationToken);


            // Creating new map with received parameters
            var mapDb = new MapDb
            {
                Size = request.MapSize,
                WinningChain = request.MapWinningChain,
                GameId = gameDb.Id
            };
            await _context.Maps.AddAsync(mapDb, cancellationToken);

            //  Creating new map cells
            var cells = CreateCellsForMap(mapDb);
            var cellDbs = cells as CellDb[] ?? cells.ToArray();
            await _context.AddRangeAsync(cellDbs, cancellationToken);

            var game = await _context.Games
                .Where(g => g.Id == gameDb.Id)
                .SingleOrDefaultAsync(cancellationToken);

            var cellList = cellDbs.Select(item => 0).ToList();

            var gameDto = new Game
            {
                Id = game.Id,
                MapId = request.MapSize,
                PlayerOne = Convert.ToInt32(request.PlayerOne),
                PlayerTwo = Convert.ToInt32(request.PlayerTwo),
                TicTacList = cellList
            };

            try
            {
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Ok(_mapper.Map<Game>(gameDto));
            }
            catch (DbUpdateException ex)
            {
                return Result.Fail<Game>(ex.Message);
            }
        }


        /// <summary>
        ///     Check for the existence of players in the database.
        /// </summary>
        private static async Task CheckPlayerIdForExistenceAsync(
            TicTacDbContext context, string id, CancellationToken cancellationToken)
        {
            var haunterOne = await context.Haunters
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
            if (haunterOne != null) return;
            Result.Fail<Game>($"There is no player's id {id} in database");
        }

        /// <summary>
        ///     The method initializes add cells to DB that represents the game map.
        /// </summary>
        /// <param name="mapDb">Map database projection</param>
        private static IEnumerable<CellDb> CreateCellsForMap(MapDb mapDb)
        {
            var cells = new List<CellDb>();
            var mapId = mapDb.Id;

            for (var i = 0; i < mapDb.Size; i++)     // lines
                for (var j = 0; j < mapDb.Size; j++) // columns
                {
                    var cell = new CellDb
                    {
                        MapId = mapId,
                        X = i,
                        Y = j,
                        TicTac = 0
                    };

                    cells.Add(cell);
                }

            return cells;
        }

        /// <summary>
        ///     Creating fake haunters if HaunterDb is empty.
        /// </summary>
        /// <param name="context">Db context.</param>
        /// <param name="cnt">Count of faker.</param>
        private static void Seed(DbContext context, int cnt)
        {
            var faker = new Faker<HaunterDb>()
                .RuleFor(r => r.Id, f => f.IndexFaker.ToString())
                .RuleFor(r => r.FullName, f => f.Person.FullName)
                .RuleFor(r => r.Password, f => f.Internet.Password(8))
                .RuleFor(r => r.UserName, f => f.Person.UserName);

            var haunter = faker.Generate(cnt);
            context.AddRange(haunter);
            context.SaveChanges();
        }
    }
}