using Microsoft.AspNetCore.Identity;
using System;

namespace EP.Balda.Logic.Models
{
    /// <summary>
    /// <c>Player</c> model class.
    /// Represents a Player.
    /// </summary>
    public class Player : IdentityUser<string>
    {
        /// <summary>
        /// Login property. Represents player's login.
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// Password property. Represents player's password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Score property. Represents player's score.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Created property. Represents the data when player profile was created.
        /// </summary>
        public DateTime Created { get; set; }
    }
}