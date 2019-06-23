using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Balda.Data.Context;
using EP.Balda.Data.Models;
using EP.Balda.Logic.Commands;
using EP.Balda.Logic.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EP.Balda.Logic.Handlers
{
    public class CreateNewGameHandler : IRequestHandler<CreateNewGameCommand, Result<Game>>
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
            //TODO: add cells initializator
            var player = await (_context.Players
                .Where(p => p.Id == request.PlayerId)
                .FirstOrDefaultAsync<PlayerDb>());

            if (player == null)
                return Result.Fail<Game>($"There is no player's id {request.PlayerId} in database");

            var map = new Map();
            map.Size = request.MapSize;

            var mapDb = _mapper.Map<MapDb>(map);
            _context.Maps.Add(mapDb);

            await _context.SaveChangesAsync(); //remove

            InitMap(mapDb);

            var initWord = GetStartingWord(mapDb);
            PutStartingWordToMap(mapDb, initWord);

            var gameDb = new GameDb()
            {
                Map = mapDb,
                MapId = mapDb.Id,
                InitWord = initWord
            };

            _context.Games.Add(gameDb);

            gameDb.PlayerGames = new List<PlayerGame>();

            var playerGame = new PlayerGame
            {
                GameId = gameDb.Id,
                PlayerId = request.PlayerId
            };

            gameDb.PlayerGames.Add(playerGame);

            await _context.SaveChangesAsync(); //remove

            var game = await (_context.Games
                .Where(g => g.Id == gameDb.Id)
                .FirstOrDefaultAsync<GameDb>());

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
        /// <param name="size">
        ///     Parameter size requires an integer argument.
        /// </param>
        private async void InitMap(MapDb mapDb)
        {
            var cells = new List<CellDb>();

            for (var i = 0; i < mapDb.Size; i++)     // lines
                for (var j = 0; j < mapDb.Size; j++) // columns
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

            await _context.AddRangeAsync(cells);
        }

        /// <summary>
        ///     The method puts the starting word on the map.
        /// </summary>
        /// <param name="word">Parameter word requires string argument.</param>
        public async void PutStartingWordToMap(MapDb mapDb, string word)
        {
            var center = mapDb.Size / 2;
            var charDestination = 0;

            word = word.Trim();
            foreach (var letter in word)
            {
                var cellDb = mapDb.Cells.Where(c => c.X == charDestination & c.Y == center).FirstOrDefault();
                charDestination++;
                cellDb.Letter = letter;
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        ///     The method gets the initial word.
        /// </summary>
        private string GetStartingWord(MapDb mapDb)
        {
            var word = "";
            var sizeRepo = _context.WordsRu.CountAsync();
            
            while (word.Length != mapDb.Size)
                word = _context.WordsRu.Where(w => w.Id == RandomWord(sizeRepo.Result)).FirstOrDefaultAsync().Result.Word;

            //word = _dataRepository.Get(RandomWord(sizeRepo));
            return word;
        }

        /// <summary>
        ///     The method choose random initial word.
        /// </summary>
        private int RandomWord(int size)
        {
            var rnd = new Random();
            var next = rnd.Next(0, size - 1);
            return next;
        }
    }
}