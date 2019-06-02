using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using Bogus.DataSets;
using EP.Hangman.Data;


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
        private string[] AllWords;

        /// <summary>
        /// Faker's settings
        /// </summary>
        public Word()
        {
            AllWords = new Lorem().Words(15);
        }

        /// <summary>
        /// Method select one word from List of words (Use random)
        /// </summary>
        /// <returns>One word</returns>
        public string GetNewWord()
        {
            return AllWords[new Random().Next(0, AllWords.Length)].ToUpper();
        }
    }
}
