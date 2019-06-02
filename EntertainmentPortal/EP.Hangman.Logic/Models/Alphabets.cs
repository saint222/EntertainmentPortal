using System.Collections.Generic;

namespace EP.Hangman.Logic.Models
{
    /// <summary>
    /// It gives alphabet's data for game
    /// </summary>
    public class Alphabets
    {
        public Alphabets()
        {
            Alphabet = new List<string>();
        }

        /// <summary>
        /// Property stores alphabet
        /// </summary>
        private List<string> Alphabet { get; set; }

        /// <summary>
        /// Method needs if we'll use more than one language
        /// </summary>
        /// <returns>English alphabet</returns>
        public List<string> EnglishAlphabet()
        {
            Alphabet.Clear();

            for (char letter = 'A'; letter <= 'Z'; letter++)
            {
                Alphabet.Add(letter.ToString());
            }
            return Alphabet;
        }
    }
}   
