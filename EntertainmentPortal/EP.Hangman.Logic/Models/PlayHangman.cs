using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.Hangman.Logic.Models
{
    public class PlayHangman
    {
        private const int ATTEMPTS = 6;
        private int _userAttempts = 0;
        public string PickedWord { get; set; }
        public List<string> CorrectLetters { get; set; }
        public List<string> Alphabet { get; set; }

        public PlayHangman()
        {
            
        }

        public int UserAttempts
        {
            get { return ATTEMPTS - _userAttempts; }
        }

        public string PlayGame(string letter)
        {

            if (_userAttempts < ATTEMPTS)
            {
                Alphabet.Remove(letter);

                if (PickedWord.Contains(letter))
                {
                    CorrectLetters[PickedWord.IndexOf(letter)] = letter;
                    return "Correct letter";
                }
                else
                {
                    _userAttempts++;
                    return "Wrong letter";
                }
            }
            else
            {
                return "Game over";
            }

        }

        private string ListToString(List<string> list)
        {
            string result = null;
            foreach (var letter in list)
            {
                result += letter;
            }

            return result;
        }
    }
}
