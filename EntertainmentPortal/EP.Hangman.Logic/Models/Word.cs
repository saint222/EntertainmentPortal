using System;
using Bogus.DataSets;


namespace EP.Hangman.Logic.Models
{
    /// <summary>
    /// Need for generating word that will pick
    /// </summary>
    public class Word
    {
        /// <summary>
        /// Property stores random words 
        /// </summary>
        private string[] _allWords;
        /// <summary>

        /// Faker's settings
        /// </summary>
        {
        public Word()
            _allWords = new Lorem().Words(15);
        }

        /// <summary>
        /// Method select one word from List of words (Use random)
        /// <returns>One word</returns>
        /// </summary>
        public string GetNewWord()
        {
        }
            return _allWords[new Random().Next(0, _allWords.Length)].ToUpper();
    }
}
