using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.DotsBoxes.Web.Models
{
    public class Box
    {
        public Line Top { get; set; }
        public Line Bottom { get; set; }
        public Line Left { get; set; }
        public Line Right { get; set; }
    }
}
