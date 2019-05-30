using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Hangman.Logic.Models
{
    public class HangmanDataResponse
    {
        public IEnumerable<string> CorrectLettersTempData { get; set; }

        public string PickedWord { get; set; }

        public IEnumerable<string> AlphabetTempData { get; set; }
    }
}
