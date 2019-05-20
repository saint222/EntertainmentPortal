using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.Sudoku.Web.Models
{
    public class Session
    {
        public int Id { get; set; }
        public Player Player { get; set; } // is it necessary?
        public Enums.DifficultyLevel Level { get; set; } 
        public bool IsOver { get; set; }
        public double Duration { get; set; }
    }
}
