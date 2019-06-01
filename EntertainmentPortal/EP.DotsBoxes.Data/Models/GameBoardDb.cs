namespace EP.DotsBoxes.Data.Models
{
    /// <summary>
    /// The model <c>GameBoardDb</c> class.
    /// Represents a playing field (game board).
    /// </summary>
    public class GameBoardDb
    {
        /// <summary>
        /// Id property.
        /// </summary>
        /// <value>
        /// A value represents unique game board's Id.
        /// </value>
        public int Id { get; set; }
        /// <summary>
        /// Rows property.
        /// </summary>
        /// <value>
        /// The value represents the row of the playing field.
        /// </value>
        public int Rows { get; set; }

        /// <summary>
        /// Columns property.
        /// </summary>
        /// <value>
        /// The value represents the column of the playing field.
        /// </value>
        public int Columns { get; set; }
    }
}
