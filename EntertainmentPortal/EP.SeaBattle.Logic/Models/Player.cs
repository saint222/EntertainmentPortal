using System;
using System.Collections.Generic;
using System.Text;

namespace EP.SeaBattle.Logic.Models
{
    public class Player
    {
        public string Id { get; set; }

        /// <summary>
        /// Nickname
        /// </summary>
        public string NickName { get; set; }
        public string GameId { get; set; }
        public ICollection<Ship> Ships { get; set; }
    }
}
