using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace EP.Sudoku.Logic.Models
{
    public class Cell
    {
        public int Id { get; set; }
        public int? Value { get; set; }
        public bool IsStartValue { get; set; }
        public Point Coordinate { get; set; }
    }
}
