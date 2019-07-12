namespace EP.TicTacToe.Logic.Models
{
    /// <summary>
    ///     Entity of Cells from BL.
    /// </summary>
    public class Cell
    {
        /// <summary>
        ///     Id property. Represents Id of CellDb in game map.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     X property. Represents X coordinate of the cell in game map.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        ///     Y property. Represents Y coordinate of the cell in game map.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        ///     Char tic-tac-toe property. Represents char to store in the cell.
        /// </summary>
        /// <remarks>
        ///     <c>1</c> corresponds to a cross, <c>-1</c> zero, <c>0</c> empty field
        /// </remarks>
        public int TicTac { get; set; }

        /// <summary>
        ///     Map external key.
        /// </summary>
        /// <remarks>
        ///     The Nullable property is used to highlight that there may be no records in a related table.
        /// </remarks>
        public int? MapId { get; set; }

        /// <remarks>
        ///     Navigation property of <c>MapDb</c>.
        /// </remarks>
        public Map Map { get; set; }
    }
}