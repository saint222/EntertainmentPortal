namespace EP.DotsBoxes.Logic.Models
{
    /// <summary>
    /// The model <c>GameBoard</c> class.
    /// Represents a playing field (game board).
    /// </summary>
    public class GameBoard
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
