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
			this._players = new List<Player>();
			this._rules = new Rules();
			this._words = new Dictionary<Player, List<Word>>();
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
		public void InsertWord(Player player, string word)
		{

		}
    }
}
