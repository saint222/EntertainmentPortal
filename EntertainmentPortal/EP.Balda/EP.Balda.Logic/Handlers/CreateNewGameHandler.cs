using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Balda.Data.Context;
using EP.Balda.Data.Models;
using EP.Balda.Logic.Commands;
using EP.Balda.Logic.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EP.Balda.Logic.Handlers
{
    public class
        CreateNewGameHandler : IRequestHandler<CreateNewGameCommand, Result<Game>>
    {
        private readonly BaldaGameDbContext _context;
        private readonly IMapper _mapper;

        public CreateNewGameHandler(BaldaGameDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<Game>> Handle(CreateNewGameCommand request, CancellationToken cancellationToken)
        {
            var player = await _context.Users
                .Where(p => p.Id == request.PlayerId)
                .FirstOrDefaultAsync(cancellationToken);

            if (player == null)
                return Result.Fail<Game>(
                    $"There is no player's id {request.PlayerId} in database");

            var mapDb = new MapDb { Size = request.MapSize };

            await _context.Maps.AddAsync(mapDb);

            await _context.SaveChangesAsync(); //remove

            var cells = CreateCellsForMap(mapDb);
            await _context.AddRangeAsync(cells);

            await _context.SaveChangesAsync();

            string initWord = GetStartingWord(mapDb);
            await PutStartingWordToMap(mapDb, initWord);

            await _context.SaveChangesAsync();

            var gameDb = new GameDb
            {
                Map = mapDb,
                MapId = mapDb.Id,
                InitWord = initWord
            };

            await _context.Games.AddAsync(gameDb);

            gameDb.PlayerGames = new List<PlayerGame>();

            var playerGame = new PlayerGame
            {
                GameId = gameDb.Id,
                PlayerId = request.PlayerId
            };

            gameDb.PlayerGames.Add(playerGame);

            await _context.SaveChangesAsync(); //remove

            var game = await _context.Games
                .Where(g => g.Id == gameDb.Id)
                .Include(g => g.Map)
                .SingleOrDefaultAsync(cancellationToken);

            try
            {
                await _context.SaveChangesAsync();

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
        public List<CellDb> CreateCellsForMap(MapDb mapDb)
        {
            var cells = new List<CellDb>();

            for (int i = 0; i < mapDb.Size; i++)     // lines
                for (int j = 0; j < mapDb.Size; j++) // columns
                {
                    var cell = new CellDb
                    {
                        X = i,
                        Y = j,
                        Letter = null,
                        Map = mapDb,
                        MapId = mapDb.Id
                    };

                    cells.Add(cell);
                }

            return cells;
        }

        /// <summary>
        ///     The method puts the starting word on the map.
        /// </summary>
        /// <param name="mapDb">Map database projection</param>
        /// <param name="word">Parameter word requires string argument.</param>
        public async Task PutStartingWordToMap(MapDb mapDb, string word)
        {
            int center = mapDb.Size / 2;
            int charDestination = 0;

            word = word.Trim();
            foreach (char letter in word)
            {
                var cellDb =
                    mapDb.Cells.SingleOrDefault(
                        c => (c.X == charDestination) & (c.Y == center));

                charDestination++;
                cellDb.Letter = letter;
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// The method gets the initial word.
        /// </summary>
        /// <param name="mapDb">Map database projection</param>
        /// <returns></returns>
        private string GetStartingWord(MapDb mapDb)
        {
            string word = "";
            int sizeRepo = _context.Words.Count();

            while (word.Length != mapDb.Size)
                word = _context.Words.Where(w => w.Id == RandomWord(sizeRepo))
                    .FirstOrDefault().Word.ToUpper();

            return word;
        }

        /// <summary>
        /// The method choose random initial word.
        /// </summary>
        /// <param name="size">Word length</param>
        /// <returns></returns>
        private int RandomWord(int size)
        {
            var rnd = new Random();
            int next = rnd.Next(1, size);
            return next;
        }
    }
}