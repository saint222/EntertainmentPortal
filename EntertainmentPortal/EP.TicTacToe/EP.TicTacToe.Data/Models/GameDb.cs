using System.Collections.Generic;

namespace EP.TicTacToe.Data.Models
{
    /// <summary>
    ///     Entity of Games.
    /// </summary>
    public class GameDb
    {
        /// <summary>
        ///     Id property. Represents Id of GameDb.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     MapDb property. Navigation property of MapDb.
        ///     Used for one-to-one relationships.
        /// </summary>
        public MapDb Map { get; set; }

        /// <summary>
        ///     PlayerGames property. Used for many-to-many relationships.
        /// </summary>
        public IList<PlayerGameDb> PlayerGames { get; set; }

        /// <summary>
        ///     StepDb property. Used for one-to-many relationships.
        /// </summary>
        public IList<StepDb> Steps { get; set; }
    }
}