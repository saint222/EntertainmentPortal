using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EP.Hagman.Data;
using EP.Hagman.Logic.Interfaces;

namespace EP.Hangman.Logic.Models
{
    /// <summary>
    /// Basic Hangman's logic
    /// </summary>
    public class PlayHangman
    {
        /// <summary>
        /// The field is temporary data storage.
        /// </summary>
        private HangmanTemporaryData _data;
        /// <summary>
        /// The field strores letter which entered by user
        /// </summary>
        private string _enteredLetter;
        /// <summary>
        /// The field stores quantity of user's attempts
        /// </summary>
        private const int ATTEMPTS = 6;

        public PlayHangman(HangmanTemporaryData data, string enteredLetter)
        {
            _data = data;
            _enteredLetter = enteredLetter;
        }
        /// <summary>
        /// Method returns number of user's attempts that he can use yet. 
        /// </summary>
        public int UserAttempts
        {
            get { return ATTEMPTS - _data.temp.UserAttempts; }
        }
        /// <summary>
        /// Game's basic logic
        /// </summary>
        /// <returns>Returned HangmanTemporaryData object or NULL</returns>
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
