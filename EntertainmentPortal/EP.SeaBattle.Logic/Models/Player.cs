using System;
using System.Collections.Generic;
using System.Text;

namespace EP.SeaBattle.Logic.Models
{
    public class Player
    {
        //TODO Change to Guid
        public string Id { get; set; }

        /// <summary>
        /// Nickname
        /// </summary>
        public string NickName { get; set; }

        public override bool Equals(object obj)
        {
            var player = obj as Player;
            if (player == null)
                return false;
            return this.Id == player.Id && this.NickName == player.NickName;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
