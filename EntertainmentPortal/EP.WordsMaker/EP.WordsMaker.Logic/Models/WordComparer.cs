using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.WordsMaker.Logic.Models
{
	/// <summary>
	/// Compares two words. 
	/// Checks if a player’s word 
	/// can be composed of the letters of a keyword.
	/// </summary>
	public class WordComparer
	{
		public bool CompareWord(string keyWord, string playerWord)
		{
			WordObj _keyWord = new WordObj(keyWord);
			WordObj _playerWord = new WordObj(playerWord);

			if(_keyWord.Length >= _playerWord.Length)
			{
				if(_keyWord.LettersCount >= _playerWord.LettersCount)
				{
					foreach (char letter in _playerWord.Letters)
					{
						if(_keyWord.Contains(letter) && (_keyWord.CharCount(letter) >= _playerWord.CharCount(letter)))
						{

						}
						else
						{
							return false;
						}
					}
					return true;
				}
			}
			return false;				
		}
	}
}
