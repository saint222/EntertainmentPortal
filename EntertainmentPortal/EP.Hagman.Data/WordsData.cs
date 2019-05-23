using System;
using System.Collections.Generic;
using System.Text;
using EP.Hagman.Data.Models;

namespace EP.Hagman.Data
{
    public static class WordsData
    {       
        private static List<WordDB> _wordsStorage = new List<WordDB>
        {
            {new WordDB("angry")},
            {new WordDB("fascinating")},
            {new WordDB("wonderful")},
            {new WordDB("environment")},
            {new WordDB("zombie")},
            {new WordDB("neighbor")},
            {new WordDB("investigate")},
            {new WordDB("mistake")},
            {new WordDB("nature")},
        };

        public static WordDB Word
        {
            get
            {
                var rnd = new Random();
                return _wordsStorage[rnd.Next(0, _wordsStorage.Count)];
            }
        }

        public static List<WordDB> AllWords => _wordsStorage;
    }
}
