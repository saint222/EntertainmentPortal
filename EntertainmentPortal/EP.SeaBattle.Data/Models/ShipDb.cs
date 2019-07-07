using EP.SeaBattle.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EP.SeaBattle.Data.Models
{
    public class ShipDb
    {
        public ShipDb()
        {
            Cells = new List<CellDb>();
        }
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public ShipRank Rank { get; set; }


        virtual public ICollection<CellDb> Cells { get; set; }
        public string PlayerId { get; set; }
        public PlayerDb Player { get; set; }
    }
}
