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
        ///     <c>Map</c>
        /// </summary>
        public int MapId { get; set; }

        /// <summary>
        ///     <c>First Player</c> 
        /// </summary>
        public int PlayerOne { get; set; }

        /// <summary>
        ///     <c>Second Player</c> 
        /// </summary>
        public int PlayerTwo { get; set; }

        /// <summary>
        ///     Values of all <c>Cells</c> on the map in the current game.
        /// </summary>
        public List<int> TicTacList { get; set; }
    }
}