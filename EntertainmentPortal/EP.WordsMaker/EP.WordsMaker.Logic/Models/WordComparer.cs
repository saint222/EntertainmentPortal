using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.WordsMaker.Logic.Models
{
	public class WordComparer
	{
		public bool CompareWord(string keyWord, string playerWord)
		{
			Word _keyWord = new Word(keyWord);
			Word _playerWord = new Word(playerWord);

			if(_keyWord.Length >= _playerWord.Length)
			{
				if(_keyWord.LettersCount >= _playerWord.LettersCount)
				{
					bool result = true;
					foreach (char letter in _playerWord.Letters)
					{
						if(_keyWord.Contains(letter) && (_keyWord.CharCount(letter) >= _playerWord.CharCount(letter)))
						{

						}
						else
						{
							result = false;
						}
					}
					return result;
				}
				return false;
			}
			else
			{
				return false;
			}					
		}
	}
}
