using System.Collections.Generic;
using EP.Balda.Logic.Models;

namespace EP.Balda.Logic.Interfaces
{
    /// <summary>
    ///     Game flow interface
    /// </summary>
    public interface IGameFlow
    {
        /// <summary>
        ///     Returns the cell by the given coordinates X and Y
        /// </summary>
        /// <param name="x">matrix element X</param>
        /// <param name="y">matrix element Y</param>
        /// <param name="map">GameMap</param>
        /// <returns>returns cell</returns>
        Cell GetCell(int x, int y, GameMap map);

        /// <summary>
        ///     The empty cell value is checked
        /// </summary>
        /// <param name="x">matrix element X</param>
        /// <param name="y">matrix element Y</param>
        /// <param name="map">GameMap</param>
        /// <returns>returns true if empty</returns>
        bool IsEmptyCell(int x, int y, GameMap map);

        /// <summary>
        ///     Check if the cell is allowed to insert a new letter.
        /// </summary>
        /// <param name="x">matrix element X</param>
        /// <param name="y">matrix element Y</param>
        /// <param name="map">GameMap</param>
        /// <returns>returns true if allowed</returns>
        bool IsAllowedCell(int x, int y, GameMap map);

        /// <summary>
        ///     Check that all received letters in the form of a tuple
        ///     list of coordinates of their location on the map, comply
        ///     with the rules of the game on making words
        /// </summary>
        /// <param name="wordTuples">Tuple list of coordinates</param>
        /// <param name="map">GameMap</param>
        /// <returns>returns true if this is the correct word</returns>
        bool IsItCorrectWord(List<(int x, int y)> wordTuples, GameMap map);

        /// <summary>
        ///     Returns the word from the game map according to the transmitted list of coordinates
        /// </summary>
        /// <param name="wordTuples"></param>
        /// <param name="map"></param>
        /// <returns>The word from the game map</returns>
        string GetSelectedWord(IEnumerable<(int x, int y)> wordTuples, GameMap map);
    }
}