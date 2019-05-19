using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.Sudoku.Web.Models
{
    public class Square
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public bool IsValuePossible { get; set; }
        public bool IsEmpty { get; set; } // for the color of a square setting (white - empty, grey - set)        
        public Enum.SquareColor Color { get; set; }
    }
}
