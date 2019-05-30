using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.Sudoku.Logic.Models
{
    /// <summary>    
    /// Is used to represent the SUDOKU-gameboard.
    /// </summary>
    public class GameBoard
    {
        /// <summary>    
        /// Is used to denote the multidimensional array (9 rows and 9 columns) of cells which the gameboard consists of.
        /// </summary>
        public Cell[,] Cells = new Cell[9, 9];
    }
}
