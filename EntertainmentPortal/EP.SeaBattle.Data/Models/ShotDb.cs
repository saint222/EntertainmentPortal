using System;
using EP.SeaBattle.Common.Enums;

namespace EP.SeaBattle.Data.Models
{
    public class ShotDb
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public byte X { get; set; }
        public byte Y { get; set; }
        public string PlayerId { get; set; }
        public PlayerDb Player { get; set; }
        public ShotStatus Status { get; set; }
    }
}
