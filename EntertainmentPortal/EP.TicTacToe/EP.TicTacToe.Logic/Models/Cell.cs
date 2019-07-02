using EP.TicTacToe.Data.Models;

namespace EP.TicTacToe.Logic.Models
{
    /// <summary>
    ///     Entity of Cells.
    /// </summary>
    public class Cell
    {
        /// <summary>
        ///     Id property. Represents Id of Cell in game map.
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
        ///     Chain external key.
        /// </summary>
        /// <remarks>
        ///     The Nullable property is used to highlight that there may be no records in a related table.
        /// </remarks>
        public int? ChainId { get; set; }

        /// <summary>
        ///     Chain property. Navigation property of Chain.
        /// </summary>
        public ChainDb Chain { get; set; }
    }
}