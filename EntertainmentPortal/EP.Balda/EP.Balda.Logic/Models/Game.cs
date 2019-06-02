using System;
using EP.Balda.Data.Contracts;
using EP.Balda.Data.EntityFramework;
using EP.Balda.Logic.Interfaces;

namespace EP.Balda.Logic.Models
{
    /// <summary>
    /// <c>Game</c> model class.
    /// Represents the game process.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Internal repository to store words from DB. 
        /// </summary>
        private static readonly IWordRepository<string> _dataRepository =
            new WordRepository<string>(new BaldaDictionaryDbContext("russian_nouns.txt"));

        /// <summary>
        /// The field stores an object of the map in the game. 
        /// </summary>
        public IMap Map { get; private set; }

        /// <summary>
        /// The Game constructor. 
        /// </summary>
        /// <param name="map">
        /// Parameter map requires IMap argument.
        /// </param>
        public Game(IMap map)
        {
            Map = map;
            var initWord = GetStartingWord();
            PutStartingWordToMap(initWord);
        }

        /// <summary>
        /// The metod puts the starting word on the map.
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
        /// The method gets the initial word. 
        /// </summary>
        private string GetStartingWord()
        {
            var word = "";
            var mapSize = Map.Size;
            var sizeRepo = _dataRepository.GetAll().Count;
            while (word.Length != mapSize)
                word = _dataRepository.Get(RandomWord(sizeRepo));

            return word;
        }

        /// <summary>
        /// The method choose random initial word. 
        /// </summary>
        private static int RandomWord(int size)
        {
            var rnd = new Random();
            var next = rnd.Next(0, size - 1);
            return next;
        }
    }
}