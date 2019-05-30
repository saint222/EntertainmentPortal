using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.WordsMaker.Logic.Models
{
    /// <summary>
    /// Represents <c>Player</c> class.
    /// </summary>
    public class Player
    {
		/// <summary>
		/// ctor
		/// </summary>
		/// <param name="name"></param>
		/// <param name="identifier"></param>
		public Player(string name, int identifier)
		{
			Name = name;
			Id = identifier;
			Score = 0;
		}
        /// <summary>
        /// ID property
        /// </summary>
        /// <value>Represents unique id of player</value>
        public int Id { get; set; }
       
        /// <summary>
        /// Name property
        /// </summary>
        /// <value>Represents nickname of player</value>
        public string Name { get; set; }

        /// <summary>
        /// Score property
        /// </summary>
        /// <value>Represents score of player</value>
        public int Score { get; set; }
    }
}
