using System;
using System.Collections.Generic;
using System.Text;

namespace EP.SeaBattle.Logic.Models
{
    public class Player
    {
        /// <summary>
        /// Nickname
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// Informs whether a player can shoot
        /// </summary>
        public bool CanShoot { get; set; }
    }
}
