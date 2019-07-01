using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace EP.Balda.Data.Models
{
    /// <summary>
    /// Entity of Players.
    /// </summary>
    public class PlayerDb : IdentityUser<string>
    {
        /// <summary>
        /// Login property. Represents player's Login.
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// Password property. Represents player's Password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Score property. Represents player's Score.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// PlayerGames property. Used for many-to-many relationships.
        /// </summary>
        public IList<PlayerGame> PlayerGames { get; set; }

        /// <summary>
        /// PlayerWords property. Used for many-to-many relationships.
        /// </summary>
        public IList<PlayerWord> PlayerWords { get; set; }

        /// <summary>
        /// Created property. Represents the data when player profile was created.
        /// </summary>
        public DateTime Created { get; set; }
    }
}