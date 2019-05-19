using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.Hangman.Web.Models
{
    public class PlayHangman
    {
        private const int ATTEMPTS = 6;
        private string _pickedWord;
        private List<string> _correctLetters = new List<string>();
        private List<string> _wrongLetters = new List<string>();

        public PlayHangman()
        {
            var word = new PickedWord();
            _pickedWord = word.Content;
        }

        public void PlayGame()
        {
            for (int i = 0; i < _pickedWord.Length; i++)
            {
                _correctLetters.Add("_");
            }

            while (_wrongLetters.Capacity != ATTEMPTS)
            {
                var letter = GetLetter();
                if (_pickedWord.Contains(letter))
                {
                    _correctLetters[_pickedWord.IndexOf(letter)] = letter;
                }
                else
                {
                    _wrongLetters.Add(letter);
                }
            }
        }

        private string GetLetter()
        {
            ConsoleKeyInfo guessedLetter = Console.ReadKey();
            return guessedLetter.ToString();
        }
    }
}
