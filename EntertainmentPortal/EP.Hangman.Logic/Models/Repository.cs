using System;
using System.Collections.Generic;
using System.Text;
using EP.Hagman.Data;
using EP.Hagman.Logic.Interfaces;

namespace EP.Hangman.Logic.Models
{
    public class Repository : IRepository<HangmanTemporaryData>
    {
        public HangmanTemporaryData Create(HangmanTemporaryData data)
        {
            var item = new TemporaryData();
            item.PickedWord = new Word().GetNewWord().ToUpper();
            item.AlphabetTempData = new HangmanAlphabets().EnglishAlphabet();
            for (int i = 0; i < item.PickedWord.Length; i++)
            {
                item.CorrectLettersTempData.Add("_");
            }
            item.UserAttempts = 0;
            data.temp = item;
            return data;
        }

        public HangmanTemporaryData Select(HangmanTemporaryData data)
        { 
            return data;
        }

        public HangmanTemporaryData Update(HangmanTemporaryData item, string updatedLetter)
        {
             var temp = new PlayHangman(item, updatedLetter).PlayGame();
            return temp;
        }

        public HangmanTemporaryData Delete(HangmanTemporaryData item)
        {
            
            return item = null;
        }
 
        
    }
}
