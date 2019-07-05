using System;
using System.Collections.Generic;
using System.Text;

namespace EP.SeaBattle.Data.Models
{
    public class ShotDb
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public byte X { get; set; }
        public byte Y { get; set; }
        public string PlayerId { get; set; }
        public PlayerDb Player { get; set; }
        public string GameId { get; set; }
        public GameDb Game { get; set; }
    }
}
