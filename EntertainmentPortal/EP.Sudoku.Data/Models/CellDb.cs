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
        public int Id { get; set; }

        /// <summary>
        /// Is used to denote a value (number) from 0 till 9 for each cell filling up (DbInfo).
        /// </summary>
        /// <remarks>
        /// Nullable int is used to store an empty piece of the gameboard if still there is  no value on the surface of the cell.
        /// </remarks>        
        public int? Value { get; set; }

        /// <summary>
        /// Is used to denote, wether the value of the cell is a random-generated, or is filled up by the player (DbInfo).
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

        /// <summary>
        /// Is used to write all possible digits to the cell (DbInfo).
        /// </summary>        
        public SessionDb GameSessionDb { get; set; }
    }
}

