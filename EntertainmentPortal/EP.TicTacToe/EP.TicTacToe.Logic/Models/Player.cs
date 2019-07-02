using System.Collections.Generic;

namespace EP.TicTacToe.Logic.Models
{
    /// <summary>
    ///     Class, which specifies all properties of players in a current game.
    /// </summary>
    public class Player
    {
        /// <summary>
        ///     Property indicates unique Id-number of a player.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Property indicates unique username of a player.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///     Login property. Represents player's Login.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        ///     Property indicates password of a player to login.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///     <c>PlayerDb</c> navigation property.
        /// </summary>
        public IList<Chain> Chains { get; set; }
    }
}