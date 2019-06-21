using System.Collections.Generic;

namespace EP.Balda.Data.Models
{
    /// <summary>
    ///     Entity of Games.
    /// </summary>
    public class GameDb
    {
        /// <summary>
        ///     Id property. Represents Id of Game.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     Map property. Navigation property of Map.
        /// </summary>
        public MapDb Map { get; set; }

        /// <summary>
        ///     MapId property. Represents Id of Map.
        /// </summary>
        public long MapId { get; set; }

        /// <summary>
        ///     PlayerGames property. Used for many-to-many relationships.
        /// </summary>
        public IList<PlayerGame> PlayerGames { get; set; }
    }
}