using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.Sudoku.Web.Helpers
{
    public class MyRandom
    {
        public static Random Rnd { get; set; }

        static MyRandom() // static constructor fo unique numbers generation
        {
            Rnd = new Random();
        }
    }
}
