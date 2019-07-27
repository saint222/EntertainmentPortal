using EP.SeaBattle.Common.Enums;
using System;

namespace EP.SeaBattle.Data.Models
{
    public class CellDb
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public byte X { get; set; }
        public byte Y { get; set; }
        public CellStatus Status { get; set; }
        public string ShipId { get; set; }
        public ShipDb Ship { get; set; }
    }
}
