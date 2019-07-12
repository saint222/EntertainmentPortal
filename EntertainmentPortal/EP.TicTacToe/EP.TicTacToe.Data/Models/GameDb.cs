using System.Collections.Generic;

namespace EP.TicTacToe.Data.Models
{
    /// <summary>
    ///     Entity of Games.
    /// </summary>
    public class GameDb
    {
        /// <summary>
        ///     <c>Id</c> property. Represents Id of GameDb.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     <c>MapDb</c> navigation property. Used for one-to-one relationships.
        /// </summary>
        public MapDb Map { get; set; }

        /// <summary>
        ///     <c>PlayerDb</c> navigation property. Used for one-to-one relationships.
        /// </summary>
        public PlayerDb Player { get; set; }

        /// <summary>
        ///     <c>StepResultDb</c> navigation property. Used for one-to-many relationships.
        /// </summary>
        public IList<StepResultDb> StepResults { get; set; }
    }
}