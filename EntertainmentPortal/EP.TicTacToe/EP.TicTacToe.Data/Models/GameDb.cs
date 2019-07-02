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
        ///     <c>MapDb</c> navigation property. Used for one-to-many relationships.
        /// </summary>
        public IList<MapDb> Maps { get; set; }

        /// <summary>
        ///     <c>PlayerDb</c> navigation property. Used for one-to-many relationships.
        /// </summary>
        public IList<PlayerDb> Players { get; set; }

        /// <summary>
        ///     <c>ChainDb</c> navigation property. Used for one-to-many relationships.
        /// </summary>
        public IList<ChainDb> Chains { get; set; }
    }
}