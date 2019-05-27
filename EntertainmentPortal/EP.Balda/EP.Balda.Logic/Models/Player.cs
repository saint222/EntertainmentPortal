// Filename: Player.cs
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
        /// Id property.
        /// </summary>
        /// <value>
        /// A value represents unique player's Id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Name property.
        /// </summary>
        /// <value>
        /// A value represents player's nickname.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Login property.
        /// </summary>
        /// <value>
        /// A value represents player's login.
        /// </value>
        public string Login { get; set; }

        /// <summary>
        /// Password property.
        /// </summary>
        /// <value>
        /// A value represents player's password.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Words property.
        /// </summary>
        /// <value>
        /// A value represents the list of words that player already entered in the match.
        /// </value>
        /// <seealso cref="Word">
        public IEnumerable<Word>
            Words { get; set; } //words this player guessed per one game
    }
}