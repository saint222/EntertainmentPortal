using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace EP.DotsBoxes.Data.Models
{
    public class PlayerDb
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Color Color { get; set; }
        public int Score { get; set; }
        public DateTime Created { get; set; }

    }

}
