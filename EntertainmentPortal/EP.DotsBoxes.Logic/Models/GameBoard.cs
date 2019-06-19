using System.Collections.Generic;

namespace EP.DotsBoxes.Logic.Models
{
    /// <summary>
    /// <c>GameBoard</c> model class.
    /// Represents a playing field (game board).
    /// </summary>
    public class GameBoard
    {
        /// <summary>
        /// Id property. Represents unique game board's Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Rows property. Represents the row of the playing field.
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        /// Columns property. Represents the column of the playing field.
        /// </summary>
        public int Columns { get; set; }

        /// <summary>
        /// Cells property. Represents a list of cells of the playing field.
        /// </summary>
        public List<Cell> Cells { get; set; }


    }
}
