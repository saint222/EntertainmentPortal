using EP.SeaBattle.Common.Enums;
using System;
using System.Collections.Generic;

namespace EP.SeaBattle.Data.Models
{
    public class ShipDb
    {
        //TODO Change to Guid
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public ShipRank Rank { get; set; }

        public IEnumerable<CellDb> Cells { get; set; }

        public PlayerDb Player { get; set; }

        public GameDb Game { get; set; }
    }
}
