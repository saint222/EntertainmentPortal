﻿using System;
using System.Collections.Generic;
using System.Text;
using EP.Hagman.Data.Models;

namespace EP.Hagman.Data
{
    public class HangmanAlphabetData
    {
        public HangmanAlphabetData()
        {
            Alphabet = new AlphabetData().Alphabet;

        }
        public List<string> Alphabet { get; set; }

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
