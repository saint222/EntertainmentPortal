using System;
using System.Collections.Generic;
using System.Text;
using EP.Hagman.Data;
using EP.Hagman.Data.Models;
using EP.Hagman.Logic.Interfaces;

namespace EP.Hangman.Logic.Models
{
    public class Word
    {
        public List<WordData> Words { get; set; }
        public string Name { get; set; }

        public Word()
        {
            Words = new HangmanWordsData().AllWords;
        }

        public Word(HangmanWordsData data)
        {
            Words = data.AllWords;
        }

        public string GetNewWord()
        {
            return Words[new Random().Next(0, Words.Count)].Name;
        }
    }
}
