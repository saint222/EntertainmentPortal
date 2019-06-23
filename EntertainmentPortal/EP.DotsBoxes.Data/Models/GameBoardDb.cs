using System;
using System.Collections.Generic;

namespace EP.DotsBoxes.Data.Models
{
    /// <summary>
    /// <c>GameBoard</c> model class.
    /// Represents a playing field (game board).
    /// </summary>
    public class GameBoardDb
    {
        /// <summary>
        /// Id property. Stores unique game board's Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Rows property. Stores the row of the playing field.
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        /// Columns property. Stores the column of the playing field.
        /// </summary>
        public int Columns { get; set; }

        /// <summary>
        /// Cells property. Stores a list of cells of the playing field.
        /// </summary>
        public List<CellDb> Cells { get; set; }

        /// <summary>
        /// Created property. Stores date of creation / registration of the playing field.
        /// </summary>
        /// <seealso cref="System">
        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}
