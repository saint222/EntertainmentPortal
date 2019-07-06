using System;

namespace EP.DotsBoxes.Data.Models
{
    /// <summary>
    /// <c>Cell</c> model class.
    /// Represents a playing field cell.
    /// </summary>
    public class CellDb
    {
        /// <summary>
        /// Id property. Stores unique cell's Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// GameBoardId property. Stores unique game board's Id where the cell is located.
        /// </summary>
        public int GameBoardId { get; set; }

        /// <summary>
        /// Rows property. Stores the row of the playing field where the cell is located.
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Column property. Stores the сolumn of the playing field where the cell is located.
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        /// Top property. Stores the top side of the cell.
        /// </summary>
        public bool Top { get; set; }

        /// <summary>
        /// Bottom property. Stores the bottom side of the cell.
        /// </summary>
        public bool Bottom { get; set; }

        /// <summary>
        /// Left property. Stores the left side of the cell.
        /// </summary>
        public bool Left { get; set; }

        /// <summary>
        /// Right property. Stores the right side of the cell.
        /// </summary>
        public bool Right { get; set; }

        /// <summary>
        /// Name property. Stores player's name who made the move.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// GameBoard property. Stores the game board where the cell is located.
        /// </summary>
        public virtual GameBoardDb GameBoard { get; set; }

        /// <summary>
        /// Created property. Stores date of creation / registration of the cell.
        /// </summary>
        /// <seealso cref="System">
        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}