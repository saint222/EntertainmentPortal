using System.Collections.Generic;

namespace EP.Balda.Logic.Models
{
    /// <summary>
    /// The model <c>Player</c> class.
    /// Represents a Player.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Id property. Represents unique player's Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// NickName property. Represents player's nickname.
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// Login property. Represents player's login.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Password property. Represents player's password.
        /// </summary>
        public string Password { get; set; }

        //TODO: Consider if it's necessary
        public int Result { get; set; } 

        /// <summary>
        /// Words property. Represents the list of words that player already entered in one match
        /// </summary>
        public List<string>
            Words
        { get; set; } //words this player guessed per one game
    }
}