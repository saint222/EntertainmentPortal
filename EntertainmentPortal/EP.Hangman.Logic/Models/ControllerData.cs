using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Hangman.Logic.Models
{
    /// <summary>
    /// Request/response model
    /// </summary>
    public class ControllerData
    {
        /// <summary>
        /// Property stores User's ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Property stores guessed letters
        /// </summary>
        public IEnumerable<string> CorrectLetters { get; set; }
        
        /// <summary>
        /// Property stores alphabet letters 
        /// </summary>
        public IEnumerable<string> Alphabet { get; set; }

        /// <summary>
        /// Property stores user's attempts
        /// </summary>
        public int UserErrors { get; set; }

        /// <summary>
        /// Property stores letter entered by user
        /// </summary>
        public string Letter { get; set; }
    }
}
