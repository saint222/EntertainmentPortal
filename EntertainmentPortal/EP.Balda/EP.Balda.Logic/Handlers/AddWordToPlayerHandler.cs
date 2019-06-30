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
        AddWordToPlayerHandler : IRequestHandler<AddWordToPlayerCommand, Result<Player>>
    {
        private readonly BaldaGameDbContext _context;
        private readonly IMapper _mapper;

        public AddWordToPlayerHandler(BaldaGameDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<Player>> Handle(AddWordToPlayerCommand request,
                                                 CancellationToken cancellationToken)
        {
            var player = await _context.Players
                .Where(p => p.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (player == null)
                return Result.Fail<Player>(
                    $"There is no player's id {request.Id} in database");

            var playerGame = await _context.PlayerGames
                .FirstOrDefaultAsync(
                    p => (p.PlayerId == request.Id) & (p.GameId == request.GameId),
                    cancellationToken);

            if (playerGame == null)
                return Result.Fail<Player>(
                    $"There is no relation of player's id {request.Id} with game's id {request.GameId} in database");

            var game = await _context.Games.Where(g => g.Id == request.GameId)
                .FirstOrDefaultAsync(cancellationToken);

            var map = await _context.Maps.Where(m => m.Id == game.MapId)
                .Include(m => m.Cells).FirstOrDefaultAsync(cancellationToken);

            var cellsFormWord = new List<CellDb>();

            foreach (long id in request.CellsIdFormWord)
            {
                var cell = map.Cells.FirstOrDefault(c => c.Id == id);

                if (cell == null)
                    return Result.Fail<Player>(
                        $"There is no cell with id {id} in map's id {map.Id} in database");

                cellsFormWord.Add(cell);
            }

            if (!IsWordCorrect(cellsFormWord))
                return Result.Fail<Player>("Some empty cells are chosen");

            string word = GetSelectedWord(cellsFormWord);

            if (word == game.InitWord)
                return Result.Fail<Player>("It is initial word");

            var wordRu = _context.WordsRu
                .SingleOrDefault(w => w.Word == word);

            if (wordRu == null)
                return Result.Fail<Player>($"There is no word {word} in word database");

            var playerWordDb = new PlayerWord
            {
                PlayerId = request.Id,
                WordId = wordRu.Id,
                GameId = request.GameId
            };

            var playerWord = await _context.PlayerWords
                .FirstOrDefaultAsync(
                    pw => (pw.GameId == request.GameId) & (pw.WordId == wordRu.Id),
                    cancellationToken);

            if (playerWord != null)
                return Result.Fail<Player>("Word has already been used");

            //TODO Add when create player

            if (player.PlayerWords == null)
                player.PlayerWords = new List<PlayerWord>();

            player.PlayerWords.Add(playerWordDb);

            try
            {
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Ok(_mapper.Map<Player>(player));
            }
            catch (DbUpdateException ex)
            {
                return Result.Fail<Player>(ex.Message);
            }
        }

        /// <summary>
        /// The method checks that all letters
        /// comply with the rules of the game on making words.
        /// </summary>
        /// <param name="word">Parameter requires List of Cell argument.</param>
        /// <returns>returns true if this is the correct word</returns>
        public bool IsWordCorrect(List<CellDb> word)
        {
            bool areLettersCorrect = false;

            for (int letterPosition = 0; letterPosition < word.Count; letterPosition++)
            {
                //current cell in the word
                var currentCell = word[letterPosition];

                //check that a non-empty cell is selected in the word
                if (currentCell.Letter == null)
                    return false;

                //next cell in the word
                CellDb nextCell;

                if (letterPosition < word.Count - 1)
                    nextCell = word[letterPosition + 1];
                else break;

                //check that the next cell is not empty
                if (nextCell.Letter == null)
                    return false;

                //check that the next cell is located with an offset of
                //not more than 1 and strictly horizontally from the current
                if ((Math.Abs(currentCell.X - nextCell.X) == 0) &
                    (Math.Abs(currentCell.Y - nextCell.Y) == 1))
                {
                    areLettersCorrect = true;
                    continue;
                }

                // check that the next cell is located with an offset of
                // not more than 1 and strictly vertically from the current
                if ((Math.Abs(currentCell.Y - nextCell.Y) == 0) &
                    (Math.Abs(currentCell.X - nextCell.X) == 1))
                {
                    areLettersCorrect = true;
                    continue;
                }

                areLettersCorrect = false;
            }

            return areLettersCorrect;
        }

        /// <summary>
        /// The method returns the word from the game map according to the entered cells.
        /// </summary>
        /// <param name="words">Parameter requires &lt;IEnumerable&lt;Cell&gt;&gt; argument.</param>
        /// <returns>The method returns word from the game map.</returns>
        public string GetSelectedWord(List<CellDb> words)
        {
            return words.Aggregate("", (current, cell) => current + cell.Letter);
        }
    }
}