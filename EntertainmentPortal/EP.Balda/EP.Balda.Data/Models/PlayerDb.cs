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
        /// FirstName property. Represents player's first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// LastName property. Represents player's last name.
        /// </summary>
        public string LastName { get; set; }

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