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
        AddWordToPlayerHandler : IRequestHandler<AddWordToPlayerCommand, Result<Game>>
    {
        private readonly BaldaGameDbContext _context;
        private readonly IMapper _mapper;

        public AddWordToPlayerHandler(BaldaGameDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<Game>> Handle(AddWordToPlayerCommand request, CancellationToken cancellationToken)
        {
            var player = await _context.Users
                .Where(p => p.Id == request.PlayerId)
                .FirstOrDefaultAsync(cancellationToken);

            if (player == null)
                return Result.Fail<Game>(
                    $"There is no player's id {request.PlayerId} in database");

            var playerGame = await _context.PlayerGames
                .FirstOrDefaultAsync(
                    p => (p.PlayerId == request.PlayerId && p.GameId == request.GameId),
                    cancellationToken);

            if (playerGame == null)
                return Result.Fail<Game>(
                    $"There is no relation of player's id {request.PlayerId} with game's id {request.GameId} in database");

            var game = await _context.Games.Where(g => g.Id == request.GameId)
                .FirstOrDefaultAsync(cancellationToken);

            var map = await _context.Maps.Where(m => m.Id == game.MapId)
                .Include(m => m.Cells).FirstOrDefaultAsync(cancellationToken);

            var cellsFormWord = new List<CellDb>();

            foreach (var cellFormWord in request.CellsThatFormWord)
            {
                var cell = map.Cells.FirstOrDefault(c => c.Id == cellFormWord.Id);

                if (cell == null)
                    return Result.Fail<Game>(
                        $"There is no cell with id {cellFormWord} in map's id {map.Id} in database");

                cellsFormWord.Add(cell);
            }

            if (!IsWordCorrect(request.CellsThatFormWord))
                return Result.Fail<Game>("Some empty cells are chosen");

            string wordFormed = GetSelectedWord(request.CellsThatFormWord);

            if (wordFormed == game.InitWord)
                return Result.Fail<Game>("It is initial word");

            var word = _context.Words
                .SingleOrDefault(w => w.Word == wordFormed.ToLower());

            if (word == null)
                return Result.Fail<Game>($"There is no word {wordFormed} in word database");

            var playerWord = await _context.PlayerWords
                .FirstOrDefaultAsync(
                    pw => (pw.GameId == request.GameId && pw.WordId == word.Id),
                    cancellationToken);

            if (playerWord != null)
                return Result.Fail<Game>("Word has already been used");

            var playerWordDb = new PlayerWord
            {
                PlayerId = request.PlayerId,
                WordId = word.Id,
                GameId = request.GameId,
            };

            if (game.IsPlayersTurn)
            {
                playerWordDb.IsChosenByOpponnent = false;
                game.PlayerScore += CountWordLetters(word.Word);
                game.IsPlayersTurn = false;
            }
            else
            {
                playerWordDb.IsChosenByOpponnent = true;
                game.OpponentScore += CountWordLetters(word.Word);
                game.IsPlayersTurn = true;
            }

            if (player.PlayerWords == null)
                player.PlayerWords = new List<PlayerWord>();

            player.PlayerWords.Add(playerWordDb);
            
            foreach (var cellDb in cellsFormWord)
            {
                foreach (var cellRequest in request.CellsThatFormWord)
                {
                    if(cellDb.Id == cellRequest.Id && cellDb.Letter != cellRequest.Letter)
                    {
                        cellDb.Letter = cellRequest.Letter;
                    }
                }
            }

            if (IsGameOver(map.Cells.ToList()))
            {
                game.IsGameOver = true;
            }

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
        /// The method checks that all letters
        /// comply with the rules of the game on making words.
        /// </summary>
        /// <param name="word">Parameter requires List of Cell argument.</param>
        /// <returns>returns true if this is the correct word</returns>
        public bool IsWordCorrect(List<Cell> word)
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
                Cell nextCell;

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
        /// <param name="cells">Parameter requires &lt;IEnumerable&lt;Cell&gt;&gt; argument.</param>
        /// <returns>The method returns word from the game map.</returns>
        public string GetSelectedWord(List<Cell> cells)
        {
            return cells.Aggregate("", (current, cell) => current + cell.Letter);
        }

        /// <summary>
        /// The method checks if game is over.
        /// </summary>
        /// <param name="cells">Parameter requires &lt;IEnumerable&lt;Cell&gt;&gt; argument.</param>
        /// <returns>The method returns word from the game map.</returns>
        public bool IsGameOver(List<CellDb> cells)
        {
            bool isGameOver = true;

            foreach (var cell in cells)
            {
                if(cell.Letter == null)
                {
                    isGameOver = false;
                    continue;
                }
            }

            return isGameOver;
        }

        /// <summary>
        /// The method counts letters in word.
        /// </summary>
        public int CountWordLetters(string word)
        {
            int count = 0;

            foreach (var ch in word)
            {
                count++;
            }

            return count;
        }
    }
}