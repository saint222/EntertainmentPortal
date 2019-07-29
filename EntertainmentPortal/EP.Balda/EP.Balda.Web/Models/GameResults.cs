namespace EP.Balda.Web.Models
{
    /// <summary>
    /// Represents the game status.
    /// </summary>
    public class GameResults
    {
        /// <summary>
        /// Score property. Represents player's Score.
        /// </summary>
        public int PlayerScore { get; set; }

        /// <summary>
        /// Score property. Represents player's opponent Score.
        /// </summary>
        public int OpponentScore { get; set; }

        /// <summary>
        /// PlayerName property. Represents player's userName.
        /// </summary>
        public string PlayerName { get; set; }
    }
}
