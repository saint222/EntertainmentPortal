using System;
using System.Collections.Generic;
using EP.Balda.Data.Contracts;
using EP.Balda.Data.EntityFramework;
using EP.Balda.Logic.Interfaces;

namespace EP.Balda.Logic.Models
{
    /// <summary>
    ///     Playground initializer
    /// </summary>
    public class Initially : IInitially
    {
        private static readonly IWordRepository<string> DataRepository =
            new WordRepository<string>(new BaldaDictionaryDbContext("russian_nouns.txt"));

        public IStep _gameFlow;

        public Initially(IStep gameFlow, Map map)
        {
            _gameFlow = gameFlow;
            var initWord = GetStartingWord(map);
            PutStartingWordToMap(initWord, map);
        }

        public Initially(IStep gameFlow)
        {
            _gameFlow = gameFlow;
        }

        private static string GetStartingWord(Map map)
        {
            var word     = "";
            var mapSize  = map.Size;
            var sizeRepo = DataRepository.GetAll().Count;
            while (word.Length != mapSize)
                word = DataRepository.Get(RandomWord(sizeRepo));

            return word;
        }

        private static int RandomWord(int size)
        {
            var rnd  = new Random();
            var next = rnd.Next(0, size - 1);
            return next;
        }

        #region Implementation of interfaces

        public List<Cell> InitMap(int size)
        {
            var fields = new List<Cell>();
            for (var i = 0; i < size; i++)     // lines
                for (var j = 0; j < size; j++) // column letters
                {
                    var cell = new Cell(i, j);
                    cell.Letter = null;
                    fields.Add(cell);
                }

            return fields;
        }

        /// <summary>
        ///     Put the starting word on the map
        /// </summary>
        /// <param name="word">Starting word</param>
        /// <param name="map">Game map</param>
        private static void PutStartingWordToMap(string word, Map map)
        {
            var       center          = map.Size / 2;
            var       charDestination = 0;
            IStep gameFlow        = new Step();

            word = word.Trim();
            foreach (var letter in word)
                gameFlow.GetCell(center, charDestination++, map).Letter =
                    letter;
        }

        #endregion
    }
}