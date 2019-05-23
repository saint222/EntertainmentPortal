using System;
using System.Collections.Generic;
using System.Text;
using EP.Hagman.Data.Models;

namespace EP.Hagman.Data
{
    public class HangmanTemporaryData
    {
        public HangmanTemporaryData()
        {
            TempData = new TemporaryData<string>().TempData;
            foreach (var letter in new HangmanWordsData().GetWord.Name)
            {
                TempData.Add("_");
            }

        }
        public List<string> TempData { get; set; }
    }
}
