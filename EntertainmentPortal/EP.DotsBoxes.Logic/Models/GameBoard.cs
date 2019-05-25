using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.DotsBoxes.Logic.Models
{
    /// <summary>
    /// The GameBoardDb class represents the playing field.
    /// The playing field consists of a two-dimensional array of rows and columns 
    /// representing the squares.
    /// </summary>
    public class GameBoard
    {
        /// <value>Gets/sets the value of Row.</value>
        public int Row { get; set; }
        /// <value>Gets/sets the value of Column.</value>
        public int Column { get; set; }
    }
}
