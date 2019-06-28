using System.Collections.Generic;

namespace EP.TicTacToe.Data.Models
{
    /// <summary>
    ///     Entity class, which specifies all properties of players stored in database.
    /// </summary>
    public class PlayerDb
    {
        /// <summary>
        ///     Property indicates unique Id-number of a player.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Property indicates unique nickname of a player.
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        ///     Login property. Represents player's Login.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        ///     Property indicates password of a player to login.
        /// </summary>
        public string Password { get; set; }

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