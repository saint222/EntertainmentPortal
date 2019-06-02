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
        public List<string> CorrectLettersTempData { get; set; }
        /// <summary>
        /// Property stores alphabet for game session.
        /// </summary>
        [NotMapped]
        public List<string> AlphabetTempData { get; set; }

        public string CorrectLettersTempDataAsString
        {
            get { return String.Join(',', CorrectLettersTempData); }
            set { CorrectLettersTempData = value.Split(',').ToList(); }
        }

        public string AlphabetTempDataTempDataAsString
        {
            get { return String.Join(',', AlphabetTempData); }
            set { AlphabetTempData = value.Split(',').ToList(); }
        }
    }
}
