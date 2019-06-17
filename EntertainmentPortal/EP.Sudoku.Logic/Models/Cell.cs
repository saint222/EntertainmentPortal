using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace EP.Sudoku.Logic.Models
{
    /// <summary>    
    /// Is used to represent a piece of playground area (81 cells are expected).
    /// </summary>
    public class Cell
    {
        /// <summary>    
        /// Is used to denote an identification value of a cell.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Is used to denote a value (number) from 0 till 9 for each cell filling up.
        /// </summary>    
        public int Value { get; set; }

        /// <summary>
        /// Is used to denote, wether the value of the cell is a random-generated, or is filled up by the player.
        /// </summary>               
        public bool IsStartValue { get; set; }

        /// <summary>
        /// Is used to denote cell's coordinates on the gameboard(X and Y values are expected to be used). 
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Is used to denote cell's coordinates on the gameboard (X and Y values are expected to be used). 
        /// </summary>
        public int Y { get; set; }

    }
}
