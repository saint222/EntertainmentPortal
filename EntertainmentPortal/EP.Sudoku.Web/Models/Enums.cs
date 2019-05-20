using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.Sudoku.Web.Models
{
    public class Enums
    {
        public enum DifficultyLevel
        {
            Easy,
            Normal,
            Hard
        }

        public enum CellColor
        {
            White,            
            Grey
        }
    }
}
