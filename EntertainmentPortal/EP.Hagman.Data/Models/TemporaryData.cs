using System;
using System.Collections.Generic;
using System.Text;
using EP.Hagman.Data.Models;

namespace EP.Hagman.Data
{
    /// <summary>
    /// Model stores data of session
    /// </summary>
    public class TemporaryData
    {
        public TemporaryData()
        {
            CorrectLettersTempData = new List<string>();
        }
        /// <summary>
        /// Generic collection stores correct letters
        /// </summary>
        public List<string> CorrectLettersTempData { get; set; }
        /// <summary>
        /// Property stores word which user will guess
        /// </summary>
        public string PickedWord { get; set; }
        /// <summary>
        /// Property stores number of user's attempts
        /// </summary>
        public int UserAttempts { get; set; }
        /// <summary>
        /// Property stores alphabet for game session.
        /// </summary>
        public List<string> AlphabetTempData { get; set; }
    }
}
