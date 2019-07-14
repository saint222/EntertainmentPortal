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
        /// FirstName property. Represents player's first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// LastName property. Represents player's last name.
        /// </summary>
        public string LastName { get; set; }

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