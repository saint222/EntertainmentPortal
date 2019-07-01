using System;
using System.Collections.Generic;
using System.Linq;


namespace EP.Hangman.Data.Models
{
    /// <summary>
    /// Model stores data of session
    /// </summary>
    public class GameDb
    {
        /// <summary>
        /// Property stores user's ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Property stores word which user will guess
        /// </summary>
        public string PickedWord { get; set; }

        /// <summary>
        /// Property stores number of user's attempts
        /// </summary>
        public int UserErrors { get; set; }

        /// <summary>
        /// Property stores correct letters
        /// </summary>
        public string CorrectLetters { get; set; }

        /// <summary>
        /// Property stores alphabet for game session.
        /// </summary>
        public string Alphabet { get; set; }
    }
}
