namespace EP.Balda.Data.Models
{
    /// <summary>
    ///     Table to form many-to-many relations.
    /// </summary>
    public class PlayerWord
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
        ///     WordId property. Represents Id of word.
        /// </summary>
        public int WordId { get; set; }

        /// <summary>
        ///     Word property. Navigational property of Word.
        /// </summary>
        public WordRuDb Word { get; set; }
    }
}