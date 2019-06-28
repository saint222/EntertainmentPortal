namespace EP.TicTacToe.Data.Models
{
    /// <summary>
    ///     Table to form many-to-many relations.
    /// </summary>
    public class PlayerGameDb
    {
        /// <summary>
        ///     PlayerId property. Represents Id of PlayerDb.
        /// </summary>
        public int PlayerId { get; set; }

        /// <summary>
        ///     PlayerDb property. Navigation property of PlayerDb.
        /// </summary>
        public PlayerDb Player { get; set; }

        /// <summary>
        ///     GameId property. Represents Id of GameDb.
        /// </summary>
        public int GameId { get; set; }

        /// <summary>
        ///     GameDb property. Navigation property of GameDb.
        /// </summary>
        public GameDb Game { get; set; }
    }
}