using System.Collections.Generic;
using EP.Balda.Logic.Models;

namespace EP.Balda.Logic.Interfaces
{
    /// <summary>
    /// Game flow interface
    /// </summary>
    public interface IMap
    {
        /// <summary>
        /// Size property. Stores the size of playing field.
        /// </summary>
        int Size { get; }
        
        /// <summary>
        /// Returns the cell by the given coordinates X and Y
        /// </summary>
        /// <param name="x">Matrix element X</param>
        /// <param name="y">Matrix element Y</param>
        /// <returns>The method returns <c>Cell</c></returns>
        Cell GetCell(int x, int y);

        /// <summary>
        /// The empty cell value is checked
        /// </summary>
        /// <param name="x">Matrix element X</param>
        /// <param name="y">Matrix element Y</param>
        /// <param name="map">GameMap</param>
        /// <returns>The method returns true if empty</returns>
        bool IsEmptyCell(int x, int y);

        /// <summary>
        /// Check if the cell is allowed to insert a new letter.
        /// </summary>
        /// <param name="x">matrix element X</param>
        /// <param name="y">matrix element Y</param>
        /// <returns>The method returns true if allowed</returns>
        bool IsAllowedCell(int x, int y);

    }
}