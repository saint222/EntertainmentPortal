using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.Sudoku.Web.Models
{
    //if it is necessary...
    public class Player
    {
        public int Id { get; set; }
        public string NickName { get; set; } 
        public int Score { get; set; } // duration and difLevel will be converted somehow iin score
    }
}
