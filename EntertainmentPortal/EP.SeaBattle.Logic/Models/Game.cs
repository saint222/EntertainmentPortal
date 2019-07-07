using System.Collections.Generic;

namespace EP.SeaBattle.Logic.Models
{
    public class Game
    {
        //TODO Change to Guid
        public string Id { get; set; }
        /// <summary>
        /// The game is looking for an enemy
        /// </summary>
        public bool EnemySearch { get; set; }
        /// <summary>
        /// Tells if the game is over
        /// </summary>
        public bool Finish { get; set; }

        public ICollection<Player> Players { get; set; }
        public ICollection<Shot> Shots { get; set; }
        /// <summary>
        /// The player who is allowed to move
        /// </summary>
        public string PlayerAllowedToMove { get; set; }
    }
}
