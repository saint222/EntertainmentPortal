using System;

namespace EP.Sudoku.Data.Models
{
    /// <summary>    
    /// Is used to represent a piece of playground area (81 cells are expected) (DbInfo).
    /// </summary>
    public class CellDb
    {
        /// <summary>    
        /// Is used to denote an identification value of a cell (DbInfo).
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Is used to denote a value (number) from 0 till 9 for each cell filling up (DbInfo).
        /// </summary>      
        public int Value { get; set; }
        
        /// <summary>
        /// Is used to denote cell's coordinates on the gameboard(X and Y values are expected to be used). 
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Is used to denote cell's coordinates on the gameboard (X and Y values are expected to be used). 
        /// </summary>
        public int Y { get; set; }

        public int SessionDbId { get; set; }
    }
}

