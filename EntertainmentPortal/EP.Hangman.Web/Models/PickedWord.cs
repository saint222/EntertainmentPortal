using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.Hangman.Web.Models
{
    public class PickedWord
    {
        private string _pickedWord;
        private List<string> _wordsList = new List<string>
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

        public PickedWord()
        {
            _pickedWord = ChooseWord();
        }

        public string Content => _pickedWord;

        private string ChooseWord()
        {
            var rnd = new Random( );
            return _wordsList[rnd.Next(0, _wordsList.Count)];
        }
    }
}
