using System;
using System.Collections.Generic;
using System.Text;

namespace EP.SeaBattle.Data.Models
{
    public class GameDb
    {
        public GameDb()
        {
            Players = new List<PlayerDb>();
        }
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public bool EnemySearch { get; set; }
        public bool Finish { get; set; }
        public string PlayerAllowedToMove { get; set; }
        public ICollection<PlayerDb> Players { get; set; }
    }
}
