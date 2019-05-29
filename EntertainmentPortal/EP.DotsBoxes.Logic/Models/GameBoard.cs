// Filename: GameBoard.cs
namespace EP.DotsBoxes.Logic.Models
{
    /// <summary>
    /// The model <c>GameBoard</c> class.
    /// Represents a playing field (game board).
    /// </summary>
    public class GameBoard
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
