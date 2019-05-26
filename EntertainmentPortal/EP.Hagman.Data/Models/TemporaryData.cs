using System;
using System.Collections.Generic;
using System.Text;
using EP.Hagman.Data.Models;

namespace EP.Hagman.Data
{
    public class TemporaryData
    {
        public TemporaryData()
        {
            CorrectLettersTempData = new List<string>();
        }
        public List<string> CorrectLettersTempData { get; set; }
        public string PickedWord { get; set; }
        public int UserAttempts { get; set; }
        public List<string> AlphabetTempData { get; set; }
    }
}
