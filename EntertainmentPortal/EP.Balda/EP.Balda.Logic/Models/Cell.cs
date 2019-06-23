namespace EP.Balda.Logic.Models
{
    /// <summary>
    ///     <c>Cell</c> model class.
    ///     Represents a cell of playground area.
    /// </summary>
    public class Cell
    {
        /// <summary>
        ///     Id property. Represents Id of Cell in game map.
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        ///     MapId property. Represents Id of Map.
        /// </summary>
        public long MapId { get; set; }

        /// <summary>
        ///     Map property. Navigation property of Map.
        /// </summary>
        public Map Map { get; set; }

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