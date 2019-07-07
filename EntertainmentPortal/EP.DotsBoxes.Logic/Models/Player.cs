namespace EP.DotsBoxes.Logic.Models
{
    /// <summary>
    /// <c>Player</c> model class.
    /// Represents a Player.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Id property. Represents unique player's Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name property. Represents player's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Color property. Represents player's сolor.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Score property. Represents player's score.
        /// </summary>
        public int Score { get; set; }
    }
}
