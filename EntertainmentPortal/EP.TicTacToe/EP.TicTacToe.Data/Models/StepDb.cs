namespace EP.TicTacToe.Data.Models
{
    /// <summary>
    ///     Entity of Cells.
    /// </summary>
    public class StepDb
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
        ///     Nullable char is used to store empty symbol if there is no tictactoe-char in the cell yet.
        /// </remarks>
        public char? TicTac { get; set; }

        /// <summary>
        ///     StepDb property. Navigation property of ChainDb.
        ///     Used for one-to-many relationships.
        /// </summary>
        /// <remarks>
        ///     The Nullable property is used to highlight that there may be no records in a related table.
        /// </remarks>
        public int? ChainId { get; set; }

        /// <summary>
        ///     StepDb property. Navigation property of ChainDb.
        ///     Used for one-to-many relationships.
        /// </summary>
        public ChainDb ChainDb { get; set; }
    }
}