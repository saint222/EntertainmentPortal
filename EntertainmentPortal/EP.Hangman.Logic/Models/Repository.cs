using System;
using System.Collections.Generic;
using System.Text;
using EP.Hagman.Data;
using EP.Hagman.Logic.Interfaces;

namespace EP.Hangman.Logic.Models
{
    /// <summary>
    /// CRUD
    /// </summary>
    public class Repository : IRepository<HangmanTemporaryData>
    {
        /// <summary>
        /// The method creates new object of data for game. It needs for start game's session. 
        /// </summary>
        /// <param name="data"> </param>
        /// <returns> It return new game's data object. It needs for HTTP Post request</returns>
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
        /// <summary>
        /// The method read saved data from database
        /// </summary>
        /// <returns>It return new game's data object. It needs for HTTP Get request</returns>
        public HangmanTemporaryData Select(HangmanTemporaryData data)
        { 
            return data;
        }
        /// <summary>
        /// The method works up entered letter throw basic game logic.
        /// </summary>
        /// <param name="item">Data object that saved in database</param>
        /// <param name="updatedLetter">Letter entered by user</param>
        /// <returns>Updated data object that will store in database. It needs for HTTP Update request</returns>
        public HangmanTemporaryData Update(HangmanTemporaryData item, string updatedLetter)
        {
            var temp = new PlayHangman(item, updatedLetter).PlayGame();
            return temp;
        }

        /// <summary>
        /// The method deletes data object of game session.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>It needs for HTTP Delete request</returns>
        public HangmanTemporaryData Delete(HangmanTemporaryData item)
        {
            
            return item = null;
        }
 
        
    }
}
