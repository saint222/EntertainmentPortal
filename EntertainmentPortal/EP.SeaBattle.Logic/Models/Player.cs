using System;
using System.Collections.Generic;
using System.Text;

namespace EP.SeaBattle.Logic.Models
{
    public class Player
    {
        public string NickName { get; set; }
        public string GameId { get; set; }
        public ICollection<Ship> Ships { get; set; }
    }
}
