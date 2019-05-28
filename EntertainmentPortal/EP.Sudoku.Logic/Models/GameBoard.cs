using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.Sudoku.Logic.Models
{
    /// <summary>    
    /// Represents the SUDOKU-gameboard.
    /// </summary>
    public class GameBoard
    {
        public Cell[,] Cells = new Cell[9, 9];
    }
}
