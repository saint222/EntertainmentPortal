using System.Collections.Generic;

namespace EP.Balda.Logic.Models
{
    /// <summary>
    ///     <c>Game</c> model class.
    ///     Represents the game process.
    /// </summary>
    public class Game
    {
        /// <summary>
        ///     The field stores an Id of the map in the game.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     The field stores an object of the map in the game.
        /// </summary>
        public Map Map { get; set; }

        /// <summary>
        ///     MapId property. Represents Id of Map.
        /// </summary>
        public long MapId { get; set; }

        /// <summary>
        ///     The field represents players in the game.
        /// </summary>
        public List<Player> Players { get; set; }
    }
}