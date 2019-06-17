using System;
using System.Collections.Generic;
using EP.Balda.Data.Context;
using EP.Balda.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EP.Balda.Logic.Models
{
    /// <summary>
    ///     <c>Game</c> model class.
    ///     Represents the game process.
    /// </summary>
    public class Game
    {
        /// <summary>
        ///     Context to WordsDB.
        /// </summary>
        private readonly BaldaGameDbContext _context;

        /// <summary>
        ///     The field stores an Id of the map in the game.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     The field stores an object of the map in the game.
        /// </summary>
        public IMap Map { get; }

        /// <summary>
        ///     The field represents players in the game.
        /// </summary>
        public List<Player> Players { get; set; }

        /// <summary>
        ///     The Game constructor.
        /// </summary>
        /// <param name="map">
        ///     Parameter map requires IMap argument.
        /// </param>
        /// <param name="context">
        ///     Word database context.
        /// </param>
        /// <param name="players">
        ///     Parameter players requires List&lt;Player&gt; argument.
        /// </param>
        public Game(IMap map, BaldaGameDbContext context, List<Player> players)
        {
            Map = map;
            Players = players;
            _context = context;
            var initWord = GetStartingWord();
            PutStartingWordToMap(initWord);
        }

        /// <summary>
        ///     The method puts the starting word on the map.
        /// </summary>
        /// <param name="word">Parameter word requires string argument.</param>
        public void PutStartingWordToMap(string word)
        {
            var center = Map.Size / 2;
            var charDestination = 0;

            word = word.Trim();
            foreach (var letter in word)
                Map.GetCell(center, charDestination++).Letter =
                    letter;
        }

        /// <summary>
        ///     The method gets the initial word.
        /// </summary>
        private string GetStartingWord()
        {
            var word = "";
            var mapSize = Map.Size;
            var sizeRepo = _context.WordsRu.CountAsync();
            var id = RandomWord(sizeRepo.Result);
            while (word.Length != mapSize)
                word = _context.WordsRu.FindAsync(id).Result.Word;

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