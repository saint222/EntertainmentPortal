using System;
using System.Collections.Generic;
using System.Text;

namespace EP.SeaBattle.Data.Models
{
    public class PlayerDb
    {
        public PlayerDb()
        {
            Ships = new List<ShipDb>();
        }
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string NickName { get; set; }

        public string GameId { get; set; }
        public GameDb Game { get; set; }
        public ICollection<ShipDb> Ships { get; set; }
    }
}
