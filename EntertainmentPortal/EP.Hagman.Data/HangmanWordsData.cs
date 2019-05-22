using System;
using System.Collections.Generic;
using System.Text;
using EP.Hagman.Data.Models;

namespace EP.Hagman.Data
{
    public static class HangmanWordsData
    {       
        private static List<WordData> _wordsStorage = new List<WordData>
        {
            {new WordData("angry")},
            {new WordData("fascinating")},
            {new WordData("wonderful")},
            {new WordData("environment")},
            {new WordData("zombie")},
            {new WordData("neighbour")},
            {new WordData("investigate")},
            {new WordData("mistake")},
            {new WordData("nature")},
        };

        public static WordData GetWord
        {
            get { return _wordsStorage[new Random().Next(0, _wordsStorage.Count)]; }
        }

        public static List<WordData> AllWords => _wordsStorage;
    }
}
