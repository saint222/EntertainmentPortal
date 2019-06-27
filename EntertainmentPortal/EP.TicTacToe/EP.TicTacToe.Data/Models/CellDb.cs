namespace EP.TicTacToe.Data.Models
{
    /// <summary>
    ///     Entity of Cells.
    /// </summary>
    public class CellDb
    {
        /// <summary>
        ///     Id property. Represents Id of CellDb in game map.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     MapDb property. Navigation property of MapDb.
        /// </summary>
        public MapDb MapDb { get; set; }

        /// <summary>
        ///     MapId property. Represents Id of MapDb.
        /// </summary>
        public int MapId { get; set; }

        /// <summary>
        ///     X property. Represents X coordinate of the cell in game map.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        ///     Y property. Represents Y coordinate of the cell in game map.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        ///     Letter property. Represents letter to store in the cell.
        /// </summary>
        /// <remarks>
        ///     Nullable char is used to store empty symbol if there is no tictactoe-char in the cell yet.
        /// </remarks>
        public char? TicTac { get; set; }

        /// <summary>
        ///     CellDb property. Navigation property of StepDb.
        ///     Used for many-to-one relationships.
        /// </summary>
        /// <remarks>
        ///     The Nullable property is used to highlight that there may be no records in a related table.
        /// </remarks>
        public int? StepId { get; set; }

        /// <summary>
        ///     StepDb property. Navigation property of StepDb.
        ///     Used for many-to-one relationships.
        /// </summary>
        public StepDb StepDb { get; set; }
    }
}