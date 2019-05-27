using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.WordsMaker.Logic.Models
{
	public class Word
	{
		public int MyProperty { get; set; }
		public char[] Letters
		{
			get { return _letters; }
		}

        private string _word;
		private char[] _letters;
		private Dictionary<char, int> _wordDict;

		public Word(string word)
		{
			this._word = word;
			this._letters = word.ToArray();
			_wordDict = new Dictionary<char, int>();
			foreach(char letter in _letters)
			{
				char _char = letter;
				if(_wordDict.ContainsKey(_char))
				{
					_wordDict[_char]++;
				}
				else
				{
					_wordDict.Add(_char,1);
				}
			}
		}
		public int GetCharCount(char Char)
		{
			return 0;
		}
	}
}
