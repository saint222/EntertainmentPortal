using System;

namespace EP.SeaBattle.Data.Models
{
    public class CellDb
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public byte X { get; set; }

        public byte Y { get; set; }

        public bool IsAlive { get; set; }

        public string ShipId { get; set; }
        public ShipDb Ship { get; set; }
    }
}
