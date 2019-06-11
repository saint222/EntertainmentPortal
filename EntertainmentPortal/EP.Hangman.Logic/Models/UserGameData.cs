using System.Collections.Generic;

namespace EP.Hangman.Logic.Models
{
    /// <summary>
    /// Data model for work into logic layer
    /// </summary>
    public class UserGameData
    {
        /// <summary>
        /// Property stores User's ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Property stores picked word
        /// </summary>
        public string PickedWord { get; set; }      

        /// <summary>
        /// Property stores guessed letters
        /// </summary>
        public List<string> CorrectLetters { get; set; }
        
        /// <summary>
        /// Property stores alphabet letters 
        /// </summary>
        public List<string> Alphabet { get; set; }

        /// <summary>
        /// Property stores user's attempts
        /// </summary>
        public int UserErrors { get; set; }
    }
}
