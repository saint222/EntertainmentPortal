namespace EP.Balda.Data.Models
{
    /// <summary>
    ///     Entity of Cells.
    /// </summary>
    public class CellDb
    {
        /// <summary>
        ///     Id property. Represents Id of Cell in game map.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     Map property. Navigational property of Map.
        /// </summary>
        public MapDb Map { get; set; }

        /// <summary>
        ///     MapId property. Represents Id of Map.
        /// </summary>
        public long MapId { get; set; }

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
        ///     Nullable char is used to store empty symbol if there is no letter in the cell yet.
        /// </remarks>
        public char? Letter { get; set; }
    }
}