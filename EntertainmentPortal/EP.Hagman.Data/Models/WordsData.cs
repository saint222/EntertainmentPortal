using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Hagman.Data.Models
{
    public static class WordsData
    {       
        private static List<string> _wordsList = new List<string>
        {
            "angry",
            "fascinating",
            "wonderful",
            "exciting",
            "environment",
            "zombie",
            "neighbor",
            "investigate",
            "mistake",
            "nature"
        };

        public static string AddingWord { private get; set;}

        public static string Word
        {
            get
            {
                var rnd = new Random();
                return _wordsList[rnd.Next(0, _wordsList.Count)];
            }
            set => _wordsList.Add(AddingWord);
        }
    }
}
