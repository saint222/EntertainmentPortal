using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.SeaBattle.Logic.Models
{ 
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsShip { get; set; }
        public bool IsForbidden { get; set; }
    }
}
