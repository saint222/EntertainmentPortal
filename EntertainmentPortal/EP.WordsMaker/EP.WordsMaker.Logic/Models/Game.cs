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
		private const int PLAYER_COUNT = 2;
		private Rules _rules;
		private Dictionary<Player, List<Word>> _words;
		private List<Player> _players;
		private Dictionary<Player, List<int>> _score;
		private List<Word> _allWords;
		private WordComparer _comparer;

		public string KeyWord { get; set; }

		public Rules Rules
		{
			get { return _rules; }
		}
		public List<Player> Players
		{
			get { return _players; }
		}

		public Game()
		{
			this._comparer = new WordComparer();
			this._players = new List<Player>();
			this._rules = new Rules();
			this._allWords = new List<Word>();
			this._words = new Dictionary<Player, List<Word>>();
			this._score = new Dictionary<Player, List<int>>();
		}
		public void AddPlayer(Player player)
		{
			if(this._players.Count < PLAYER_COUNT)
			{
				this._players.Add(player);
				_words.Add(player, new List<Word>());
			}
				
		}
		public void SetRules(Rules rules)
		{
			this._rules = rules;
		}
		public void ChangePlayer()
		{

		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="player"></param>
		/// <param name="word"></param>
		/// <returns></returns>
		public bool InsertWord(Player player, string word)
		{
			if(_comparer.CompareWord(KeyWord,word))
			{
				Word wrd = new Word(word);

				_words[player].Add(wrd);
				_allWords.Add(wrd);

				player.Score += _rules.ComputeScoring(word);
				return true;
			}
			else
			{
				return false;
			}
		}
    }
}
