using System.Collections.Generic;
using EP.Balda.Logic.Models;

namespace EP.Balda.Logic.Interfaces
{
    /// <summary>
    /// Game init component interface
    /// </summary>
    public interface IGame
    {
        /// <summary>
        /// Returns array of cells that represents the playuing field.
        /// </summary>
        Cell[,] InitMap(int size);

        /// <summary>
        /// Check that all received letters in the form of a tuple
        /// list of coordinates of their location on the map, comply
        /// with the rules of the game on making words
        /// </summary>
        /// <param name="cells">Tuple list of coordinates</param>
        /// <param name="map">GameMap</param>
        /// <returns>The method returns true if this is the correct word</returns>
        bool IsItCorrectWord(List<Cell> cells);

        /// <summary>
        /// Returns the word from the game map according to the transmitted list of coordinates
        /// </summary>
        /// <param name="cells"></param>
        /// <returns>The method returns 
        /// the word from the game map</returns>
        string GetSelectedWord(IEnumerable<Cell> cells);
    }
}