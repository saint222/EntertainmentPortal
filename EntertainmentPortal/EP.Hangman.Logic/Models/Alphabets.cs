using System;
using System.Collections.Generic;
using System.Text;
using EP.Hangman.Data.Models;

namespace EP.Hangman.Data
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

        private List<string> Alphabet { get; set; }

        //It's need if we'll use more than one language
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
