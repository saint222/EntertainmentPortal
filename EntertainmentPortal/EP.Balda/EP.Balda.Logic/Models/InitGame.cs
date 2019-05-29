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
    public class InitGame : IInitGame
    {
        private static readonly IWordRepository<string> DataRepository =
            new WordRepository<string>(new BaldaDictionaryDbContext("russian_nouns.txt"));

        public IGameFlow _gameFlow;

        public InitGame(IGameFlow gameFlow, GameMap map)
        {
            _gameFlow = gameFlow;
            var initWord = GetStartingWord(map);
            PutStartingWordToMap(initWord, map);
        }

        public InitGame(IGameFlow gameFlow)
        {
            _gameFlow = gameFlow;
        }

        private static string GetStartingWord(GameMap map)
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
                    var cell = new Cell(i, j, ' ');
                    fields.Add(cell);
                }

            return fields;
        }

        /// <summary>
        ///     Put the starting word on the map
        /// </summary>
        /// <param name="word">Starting word</param>
        /// <param name="map">Game map</param>
        private static void PutStartingWordToMap(string word, GameMap map)
        {
            var       center          = map.Size / 2;
            var       charDestination = 0;
            IGameFlow gameFlow        = new GameFlow();

            word = word.Trim();
            foreach (var letter in word)
                gameFlow.GetCell(center, charDestination++, map)._letter =
                    letter;
        }

        #endregion
    }
}