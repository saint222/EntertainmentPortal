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
        /// <remarks>
        /// Nullable int is used to store an empty piece of the gameboard if still there is  no value on the surface of the cell.
        /// </remarks>        
        public int? Value { get; set; }

        /// <summary>
        /// Is used to denote, wether the value of the cell is a random-generated, or is filled up by the player.
        /// </summary>               
        public bool IsStartValue { get; set; }

        /// <summary>
        /// Is used to denote cell's coordinates on the gameboard(X and Y values are expected to be used). 
        /// </summary>
        /// <remarks>
        /// The struct Point type is applied.
        /// </remarks> 
        public Point Coordinate { get; set; }

        /// <summary>
        /// Is used to write all possible digits to the cell.
        /// </summary>
        public int[] Pencil { get; set; } = new int[9];

        /// <summary>
        /// Is used to communication cell and gameboard
        /// </summary>
        public int GameBoarId { get; set; }


    }
}
