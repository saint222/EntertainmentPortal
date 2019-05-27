// Filename: Playgroynd.cs
namespace EP.Balda.Logic.Models
{
    /// <summary>
    /// The model <c>Playground</c> class.
    /// Represents a playing field (playground area).
    /// </summary>
    public class Playground
    {
        /// <summary>
        /// Cells property.
        /// </summary>
        /// <value>
        /// A value represents the array of cells that playground contains.
        /// </value>
        /// <seealso cref="Cell"/>
        public Cell[,] Cells { get; set; }

        /// <summary>
        /// Size property.
        /// </summary>
        /// <value>
        /// A value represents playground size.
        /// </value>
        public int Size { get; }

        /// <summary>
        /// Playground constructor.
        /// </summary>
        /// <remarks>
        /// At the moment it has only default size 5x5, defined by <code>Size = 5;</code>.
        /// </remarks>
        public Playground()
        {
            Size = 5;
            Cells = new Cell[Size, Size];

            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j < Size; j++) Cells[i, j] = new Cell(i, j);
            }
        }
    }
}