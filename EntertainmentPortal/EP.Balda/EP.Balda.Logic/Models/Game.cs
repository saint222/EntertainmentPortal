using System;
using System.Collections.Generic;
using EP.Balda.Data.Contracts;
using EP.Balda.Data.EntityFramework;
using EP.Balda.Logic.Interfaces;

namespace EP.Balda.Logic.Models
{
    /// <summary>
    /// Game initializer
    /// </summary>
    public class Game
    {
        private static readonly IWordRepository<string> _dataRepository =
            new WordRepository<string>(new BaldaDictionaryDbContext("russian_nouns.txt"));

        private readonly IMap _map;

        public Game(Map map)
        {
            _map = map;
            var initWord = GetStartingWord();
            PutStartingWordToMap(initWord, map);
        }
        
        private string GetStartingWord()
        {
            var word = "";
            var mapSize = _map.Size;
            var sizeRepo = _dataRepository.GetAll().Count;
            while (word.Length != mapSize)
                word = _dataRepository.Get(RandomWord(sizeRepo));

            return word;
        }

        private static int RandomWord(int size)
        {
            var rnd = new Random();
            var next = rnd.Next(0, size - 1);
            return next;
        }


        /// <summary>
        /// The metod puts the starting word on the map.
        /// </summary>
        /// <param name="word">Starting word</param>
        /// <param name="map">Game map</param>
        public void PutStartingWordToMap(string word, Map map)
        {
            var center = map.Size / 2;
            var charDestination = 0;
            
            word = word.Trim();
            foreach (var letter in word)
                _map.GetCell(center, charDestination++).Letter =
                    letter;
        }
    }
}