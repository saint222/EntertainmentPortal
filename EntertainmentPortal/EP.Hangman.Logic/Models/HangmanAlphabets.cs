using System;
using System.Collections.Generic;
using System.Text;
using EP.Hagman.Data.Models;

namespace EP.Hagman.Data
{
    public class HangmanAlphabets
    {
        public HangmanAlphabets()
        {
            Alphabet = new AlphabetData().Alphabet;
        }

        public List<string> Alphabet { get; private set; }

        //It's need if we'll use more than one language
        public List<string> EnglishAlphabet()
        {
            for (char letter = 'A'; letter <= 'Z'; letter++)
            {
                Alphabet.Add(letter.ToString());
            }
            return Alphabet;
        }
    }
}   
