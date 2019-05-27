// Filename: Cell.cs
namespace EP.Balda.Logic.Models
{
    /// <summary>
    /// The model <c>Cell</c> class.
    /// Represents a cell of playground area.
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// Letter property.
        /// </summary>
        /// <remarks>
        /// Nullable char is used to store empty symbol if there is no letter in the cell yet.
        /// </remarks>
        /// <value>
        /// A value represents letter to store in the cell.
        /// </value>
        public char? Letter { get; set; }

        /// <summary>
        /// X property.
        /// </summary>
        /// <value>
        /// A value represents X coordinate of the cell in playground.
        /// </value>
        public int X { get; }

        /// <summary>
        /// Y property.
        /// </summary>
        /// <value>
        /// A value represents Y coordinate of the cell in playground.
        /// </value>
        public int Y { get; }

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