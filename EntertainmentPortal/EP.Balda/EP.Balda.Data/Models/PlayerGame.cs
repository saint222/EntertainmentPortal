namespace EP.Balda.Data.Models
{
    /// <summary>
    ///     Table to form many-to-many relations.
    /// </summary>
    public class PlayerGame
    {
        /// <summary>
        ///     PlayerId property. Represents Id of Player.
        /// </summary>
        public long PlayerId { get; set; }

        /// <summary>
        ///     Player property. Navigational property of Player.
        /// </summary>
        public PlayerDb Player { get; set; }

        /// <summary>
        ///     GameId property. Represents Id of Game.
        /// </summary>
        public long GameId { get; set; }

        /// <summary>
        ///     Game property. Navigational property of Game.
        /// </summary>
        public GameDb Game { get; set; }
    }
}