using System.Collections.Generic;

namespace EP.Balda.Logic.Models
{
    /// <summary>
    ///     <c>Player</c> model class.
    ///     Represents a Player.
    /// </summary>
    public class Player
    {
        /// <summary>
        ///     Id property. Represents unique player's Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     NickName property. Represents player's nickname.
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        ///     Login property. Represents player's login.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        ///     Password property. Represents player's password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///     Score property. Represents player's score.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        ///     IsMoveAllowed property. Represents player's availability of making move in the game.
        /// </summary>
        public bool IsMoveAllowed { get; set; }
        
        /// <summary>
        ///     Words property. Represents the list of words that player already entered in one match
        /// </summary>
        public List<string>
            Words { get; set; } //words this player guessed per one game
    }
}