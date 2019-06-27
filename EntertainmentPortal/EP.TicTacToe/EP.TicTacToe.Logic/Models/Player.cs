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
        ///     NickName property. Represents player's Nickname.
        /// </summary>
        public string NickName { get; set; }

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