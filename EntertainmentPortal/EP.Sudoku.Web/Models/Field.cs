using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.Sudoku.Web.Models
{
    public class Field
    {
        public Square[,] Squares = new Square[9, 9];
    }
}
