using System.Collections.Generic;
using EP.Balda.Logic.Models;

namespace EP.Balda.Logic.Interfaces
{
    /// <summary>
    ///     IGame interface.
    /// </summary>
    public interface IGame
    {
        /// <summary>
        ///     The method checks that all received letters in the form of a cells,
        ///     comply with the rules of the game on making words.
        /// </summary>
        bool IsItCorrectWord(List<Cell> cells);

        /// <summary>
        ///     The method returns the word from the game map according to the transmitted list of cells.
        /// </summary>
        string GetSelectedWord(List<Cell> cells);
    }
}