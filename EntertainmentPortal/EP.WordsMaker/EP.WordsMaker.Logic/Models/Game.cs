using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.WordsMaker.Logic.Models
{
	///<summary>
	///
	///</summary>
	///<remarks>
	///
	/// </remarks>
	public class Game
    {
        /// <summary>
        /// ID property
        /// </summary>
        /// <value>Represents unique id of game</value>
        public int Id { get; set; }

		public DateTime Duration { get; set; }

		public DateTime StarTime { get; set; }

		public DateTime EndTime { get; set; }


        public string KeyWord { get; set; }

        public Player Player { get; set; }
		public int PlayerId { get; set; }


		private const int PLAYER_COUNT = 1;
		//private Rules _rules;
		private List<Word> _words;
		//private Dictionary<Player, List<int>> _score;
		private List<Word> _allWords;
		private WordComparer _comparer;

		//public Rules Rules
		//{
		//	get { return _rules; }
		//}
		//public List<Player> Players
		//{
		//	get { return _players; }
		//}

		//public Game()
		//{
		//	//this._comparer = new WordComparer();
		//	//this._players = new List<Player>();
		//	//this._rules = new Rules();
		//	//this._allWords = new List<Word>();
		//	//this._words = new Dictionary<Player, List<Word>>();
		//	//this._score = new Dictionary<Player, List<int>>();
		//}

		//public void SetRules(Rules rules)
		//{
		//	this._rules = rules;
		//}

		///// <summary>
		///// 
		///// </summary>
		///// <param name="player"></param>
		///// <param name="word"></param>
		///// <returns></returns>
		//public bool InsertWord(Player player, string word)
		//{
		//	if(_comparer.CompareWord(KeyWord,word))
		//	{
		//		Word wrd = new Word(word);

		//		_words[player].Add(wrd);
		//		_allWords.Add(wrd);

		//		player.Score += _rules.ComputeScoring(word);
		//		return true;
		//	}
		//	else
		//	{
		//		return false;
		//	}
		//}
    }
}
