using System.Collections.Generic;

namespace EP.TicTacToe.Logic.Models
{
    /// <summary>
    ///     Entity of Games.
    /// </summary>
    public class Game
    {
        /// <summary>
        ///     <c>Id</c> property. Represents Id of Game.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     <c>Map</c> navigation property.
        /// </summary>
        public IList<Map> Maps { get; set; }

        /// <summary>
        ///     <c>Player</c> navigation property.
        /// </summary>
        public IList<Player> Players { get; set; }
    }
}