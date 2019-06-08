using System.Linq;
using CSharpFunctionalExtensions;
using EP.Hangman.Data.Models;


namespace EP.Hangman.Logic.Models
{
    /// <summary>
    /// Basic Hangman's logic
    /// </summary>
    public class HangmanGame
    {
        /// <summary>
        /// The field is temporary data storage.
        /// </summary>
        private UserGameData _data;

        /// <summary>
        /// The field stores quantity of user's attempts
        /// </summary>
        private const int MAX_ERRORS = 6;

        /// <summary>
        /// Constructor needs for Unit tests
        /// </summary>
        /// <param name="data"></param>
        public HangmanGame(UserGameData data)
        {
            _data = data;
        }

        /// <summary>
        /// Game's basic logic
        /// </summary>
        /// <returns>Returned GameDb object or NULL</returns>
        public Result<UserGameData> Play(string letter)
        {
            if (_data.UserErrors < MAX_ERRORS)
            {
                _data.Alphabet.Remove(letter);

                if (_data.PickedWord.Contains(letter))
                {
                    for (int i = 0; i < _data.PickedWord.Length; i++)
                    {
                        if (_data.PickedWord.ElementAt(i).ToString() == letter)
                        {
                            _data.CorrectLetters[i] = letter;
                        }
                    }
                    return Result.Ok<UserGameData>(_data);
                }
                else
                {
                    _data.UserErrors++;
                    return Result.Ok<UserGameData>(_data);
                }
            }
            else
            {
                return Result.Fail<UserGameData>("Game over");
            }
        }
    }
}
