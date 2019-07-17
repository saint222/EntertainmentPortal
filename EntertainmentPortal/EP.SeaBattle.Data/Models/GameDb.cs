using System;
using System.Collections.Generic;
using System.Text;

namespace EP.SeaBattle.Data.Models
{
    public class GameDb
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public PlayerDb Player1 { get; set; }

        public PlayerDb Player2 { get; set; }

        public bool Finish { get; set; }

        public PlayerDb PlayerAllowedToMove { get; set; }

        public IEnumerable<ShotDb> Shots { get; set; }
    }
}
