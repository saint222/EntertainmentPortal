namespace EP.Balda.Logic.Models
{
    /// <summary>
    /// The model <c>Cell</c> class.
    /// Represents a cell of playground area.
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// X property. Represents X coordinate of the cell in playground.
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        /// Y property. Represents Y coordinate of the cell in playground.
        /// </summary>
        public int Y { get; private set; }

        /// <summary>
        /// Letter property. Represents letter to store in the cell.
        /// </summary>
        /// <remarks>
        /// Nullable char is used to store empty symbol if there is no letter in the cell yet.
        /// </remarks>
        public char? Letter { get; set; }

        /// <summary>
        /// Cell constructor with X and Y coordinates.
        /// </summary>
        /// <param name="x">
        /// Parameter x requires an integer argument.
        /// </param>
        /// <param name="y">
        /// Parameter y requires an integer argument.
        /// </param>
        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}