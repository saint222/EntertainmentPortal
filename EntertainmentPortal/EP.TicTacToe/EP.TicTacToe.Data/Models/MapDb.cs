using System.Collections.Generic;

namespace EP.TicTacToe.Data.Models
{
    /// <summary>
    ///     Entity of Maps.
    /// </summary>
    public class MapDb
    {
        /// <summary>
        ///     Id property. Represents Id of GameDb.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Size property. Represents size of playing field.
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        ///     The size of the chain of characters to win on the card.
        /// </summary>
        public int WinningChain { get; set; }

        /// <summary>
        ///     External key of class GameDb.
        /// </summary>
        public int GameId { get; set; }

        /// <summary>
        ///     GameDb property. Navigation property of GameDb.
        ///     Used for one-to-one relationships.
        /// </summary>
        public GameDb Game { get; set; }

    }
}