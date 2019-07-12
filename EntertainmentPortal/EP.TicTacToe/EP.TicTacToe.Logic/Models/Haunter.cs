using System.Collections.Generic;

namespace EP.TicTacToe.Logic.Models
{
    public class Haunter
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
    }
}