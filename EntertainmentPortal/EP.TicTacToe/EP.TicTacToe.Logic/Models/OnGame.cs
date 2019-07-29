using System.Collections.Generic;

namespace EP.TicTacToe.Logic.Models
{
    /// <summary>
    /// Get game parameters to Game controller
    /// </summary>
    public class OnGame
    {
        /// <summary>
        ///     <c>Id</c> property. Represents Id of Game.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     <c>Current moving player</c> 
        /// </summary>
        public int CurrentPlayer { get; set; }

        /// <summary>
        ///     Values of all <c>Cells</c> on the map in the current game.
        /// </summary>
        public List<int> TicTacList { get; set; }
    }
}