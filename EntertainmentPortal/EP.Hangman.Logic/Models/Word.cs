using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using EP.Hangman.Data;


namespace EP.Hangman.Logic.Models
{
    /// <summary>
    /// Need for generating word that will pick
    /// </summary>
    public class Word
    {
        /// <summary>
        /// Faker's property
        /// </summary>
        private Faker<string> _faker = new Faker<string>();
        /// <summary>
        /// Property stores random words 
        /// </summary>
        private List<string> AllWords => _faker.Generate(15);
        /// <summary>
        /// Faker's settings
        /// </summary>
        public Word()
        {
            _faker.RuleFor(prop => prop, set => set.Lorem.Word());
        }
        /// <summary>
        /// Method select one word from List of words (Use random)
        /// </summary>
        /// <returns>One word</returns>
        public string GetNewWord()
        {
            return AllWords[new Random().Next(0, AllWords.Count)];
        }
    }
}
