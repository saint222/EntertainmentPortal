using System;
using System.Collections.Generic;
using System.Text;

namespace EP.TicTacToe.Data.Models
{
    public class PlayerDb
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
        public string CountryOfLiving { get; set; }
    }
}
