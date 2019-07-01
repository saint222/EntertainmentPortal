using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

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
        public string Id { get; set; }

        ///// <summary>
        /////     Property AspNetUser. Navigation property of AspNetUsers.
        ///// </summary>
        //public AspNetUserManager<PlayerDb> AspNetUser { get; set; }

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
        ///     GameDb property. Used for one-to-many relationships.
        /// </summary>
        public int GameId { get; set; }

        /// <summary>
        ///     Navigation property of set <c>Players</c> in <c>Games</c>.
        /// </summary>
        public GameDb Game { get; set; }
    }
}