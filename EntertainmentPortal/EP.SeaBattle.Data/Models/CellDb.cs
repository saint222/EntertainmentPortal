using EP.SeaBattle.Common.Enums;
using System;

namespace EP.SeaBattle.Data.Models
{
    public class CellDb
    {
        //TODO Change to Guid
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public byte X { get; set; }

        public byte Y { get; set; }

        public CellStatus Status { get; set; }

        //TODO Change to Guid
        public string ShipId { get; set; }
        public ShipDb Ship { get; set; }
    }
}
