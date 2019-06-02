using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;


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
        public Int64 Id { get; set; }

        /// <summary>
        /// Property stores word which user will guess
        /// </summary>
        public string PickedWord { get; set; }

        /// <summary>
        /// Property stores number of user's attempts
        /// </summary>
        public int UserErrors { get; set; }

        /// <summary>
        /// Generic collection stores correct letters
        /// </summary>
        [NotMapped]
        public List<string> CorrectLetters { get; set; }

        /// <summary>
        /// Property stores alphabet for game session.
        /// </summary>
        [NotMapped]
        public List<string> Alphabet { get; set; }

        /// <summary>
        /// Property saves data from CorrectLetters to Database
        /// </summary>
        public string CorrectLettersAsString
        {
            get => String.Join(',', CorrectLetters);
            set => CorrectLetters = value.Split(',').ToList();
        }

        /// <summary>
        /// Property saves data from Alphabet to Database
        /// </summary>
        public string AlphabetAsString
        {
            get => String.Join(',', Alphabet);
            set => Alphabet = value.Split(',').ToList();
        }
    }
}
