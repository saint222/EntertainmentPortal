using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.WordsMaker.Logic.Models
{
    /// <summary>
    /// Represents <c>Word</c> class.
    /// </summary>
	public class Word
	{
        /// <summary>
        /// Letter from word property
        /// </summary>
        /// <value>Represents array latters grom word</value>
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
				if(_wordDict.ContainsKey(letter))
				{
					_wordDict[letter]++;
				}
				else
				{
					_wordDict.Add(letter, 1);
				}
			}
		}

		public int GetCharCount(char Char)
		{
			return 0;
		}
	}
}
