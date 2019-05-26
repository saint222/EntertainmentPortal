using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EP.Hagman.Data;
using EP.Hagman.Logic.Interfaces;

namespace EP.Hangman.Logic.Models
{
    public class PlayHangman
    {
        private HangmanTemporaryData _data;
        private string _enteredLetter;
        private const int ATTEMPTS = 6;

        public PlayHangman(HangmanTemporaryData data, string enteredLetter)
        {
            _data = data;
            _enteredLetter = enteredLetter;
        }

        public int UserAttempts
        {
            get { return ATTEMPTS - _data.temp.UserAttempts; }
        }

        public HangmanTemporaryData PlayGame()
        {

            if (_data.temp.UserAttempts < ATTEMPTS)
            {
                _data.temp.AlphabetTempData.Remove(_enteredLetter);

                if (_data.temp.PickedWord.Contains(_enteredLetter))
                {
                    for (int i = 0; i < _data.temp.PickedWord.Length; i++)
                    {
                        if (_data.temp.PickedWord.ElementAt(i).ToString() == _enteredLetter)
                        {
                            _data.temp.CorrectLettersTempData[i] = _enteredLetter;
                        }
                    }
                    return _data;
                }
                else
                {
                    _data.temp.UserAttempts++;
                    return _data;
                }
            }
            else
            {
                return null;
            }

        }
    }
}
