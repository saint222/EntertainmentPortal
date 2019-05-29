// Filename: GameBoardDb.cs
namespace EP.DotsBoxes.Data.Models
{
    /// <summary>
    /// The model <c>GameBoardDb</c> class.
    /// Represents a playing field (game board).
    /// </summary>
    public class GameBoardDb
    {
        /// <summary>
        /// Row property.
        /// </summary>
        /// <value>
        /// The value represents the row of the playing field.
        /// </value>
        public int Row { get; set; }

        /// <summary>
        /// Column property.
        /// </summary>
        /// <value>
        /// The value represents the column of the playing field.
        /// </value>
        public int Column { get; set; }
    }
}
