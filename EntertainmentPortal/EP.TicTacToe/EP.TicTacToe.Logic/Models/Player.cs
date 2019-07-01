namespace EP.TicTacToe.Logic.Models
{
    /// <summary>
    ///     Class, which specifies all properties of players in a current game.
    /// </summary>
    public class Player
    {
        /// <summary>
        ///     Id property. Represents unique player's Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     UserName property. Represents player's UserName.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///     Login property. Represents player's Login.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        ///     Password property. Represents player's Password.
        /// </summary>
        public string Password { get; set; }
    }
}