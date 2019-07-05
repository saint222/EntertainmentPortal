using System.Collections.Generic;

namespace EP.TicTacToe.Data.Models
{
    /// <summary>
    ///     Entity class, which specifies all properties of players stored in database.
    /// </summary>
    public class HaunterDb
    {
        /// <summary>
        ///     Property indicates unique Id-number of a player.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Property indicates full name of a player.
        /// </summary>
        public string FullName { get; set; }


        /// <summary>
        ///     Property indicates unique username of a player.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///     Property indicates password of a player to login.
        /// </summary>
        public string Password { get; set; }

        /// <remarks>
        ///     Navigation property of <c>HaunterDb</c>.
        /// </remarks>
        public List<FirstPlayerDb> FirstPlayers { get; set; }

        /// <remarks>
        ///     Navigation property of <c>HaunterDb</c>.
        /// </remarks>
        public List<SecondPlayerDb> SecondPlayers { get; set; }
    }
}