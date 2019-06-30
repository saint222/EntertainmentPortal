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
        ///     InitWord property. Represents initial word on the game map.
        /// </summary>
        public string InitWord { get; set; }

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