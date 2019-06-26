namespace EP.DotsBoxes.Logic.Models
{
    /// <summary>
    /// <c>Cell</c> model class.
    /// Represents a playing field cell.
    /// </summary>
    public class Cell
    {
       /// <summary>
        /// Id property. Represents unique cell's Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Rows property. Represents the row of the playing field where the cell is located.
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Column property. Represents the сolumn of the playing field where the cell is located.
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        /// Top property. Represents the top side of the cell.
        /// </summary>
        public bool Top { get; set; } = false;

        /// <summary>
        /// Bottom property. Represents the bottom side of the cell.
        /// </summary>
        public bool Bottom { get; set; } = false;

        /// <summary>
        /// Left property. Represents the left side of the cell.
        /// </summary>
        public bool Left { get; set; } = false;

        /// <summary>
        /// Right property. Represents the right side of the cell.
        /// </summary>
        public bool Right { get; set; } = false;

        /// <summary>
        /// Name property. Represents player's name who made the move.
        /// </summary>
        public string Name { get; set; }
    }
}
