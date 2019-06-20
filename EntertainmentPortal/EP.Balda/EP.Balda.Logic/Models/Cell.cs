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
        public int X { get; }

        /// <summary>
        ///     Y property. Represents Y coordinate of the cell in game map.
        /// </summary>
        public int Y { get; }

        /// <summary>
        ///     Letter property. Represents letter to store in the cell.
        /// </summary>
        /// <remarks>
        ///     Nullable char is used to store empty symbol if there is no letter in the cell yet.
        /// </remarks>
        public char? Letter { get; set; }

        /// <summary>
        ///     Cell constructor with X and Y coordinates.
        /// </summary>
        /// <param name="x">
        ///     Parameter x requires an integer argument.
        /// </param>
        /// <param name="y">
        ///     Parameter y requires an integer argument.
        /// </param>
        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        ///     The method checks if the cell is empty.
        /// </summary>
        /// <returns>The method retyrns true if the cell is empty.</returns>
        public bool IsEmpty()
        {
            return Letter == null;
        }
    }
}