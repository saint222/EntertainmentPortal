namespace EP.Balda.Logic.Models
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
        ///     Player property. Navigation property of Player.
        /// </summary>
        public Player Player { get; set; }

        /// <summary>
        ///     WordId property. Represents Id of word.
        /// </summary>
        public int WordId { get; set; }

        /// <summary>
        ///     Word property. Navigation property of Word.
        /// </summary>
        public WordRu Word { get; set; }
    }
}