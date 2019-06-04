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
        /// Is used to denote an identification value of a gameboard.
        /// </summary>
        public int Id { get; set; }

        /// <summary>    
        /// Is used to denote the multidimensional array (9 rows and 9 columns) of cells which the gameboard consists of.
        /// </summary>
        public Cell[,] Cells { get; set; } = new Cell[9, 9];
    }
}
