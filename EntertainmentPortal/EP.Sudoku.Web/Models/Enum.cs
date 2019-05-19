using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.Sudoku.Web.Models
{
    public class Enum
    {
        public enum DifficultyLevel
        {
            Easy,
            Normal,
            Hard
        }

        public enum SquareColor
        {
            White,            
            Grey
        }
    }
}
