using EP.Balda.Logic.Models;

namespace EP.Balda.Logic.Interfaces
{
    /// <summary>
    /// IMap interface.
    /// </summary>
    public interface IMap
    {
        /// <summary>
        /// Size property. Stores the size of playing field.
        /// </summary>
        int Size { get; }
        
        /// <summary>
        /// Returns the cell by the given coordinates X and Y.
        /// </summary>
        Cell GetCell(int x, int y);

        /// <summary>
        /// The method checks if the cell is empty.
        /// </summary>
        bool IsEmptyCell(int x, int y);

        /// <summary>
        /// The method checks if the cell is allowed to insert a new letter.
        /// </summary>
        bool IsAllowedCell(int x, int y);

    }
}