using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EP.SeaBattle.Data.Models;

namespace EP.SeaBattle.Data.Storages
{
    public static class PlayersStorage
    {
        private static readonly List<PlayerDb> _players = new List<PlayerDb>()
        {
            new PlayerDb() { Id = Guid.NewGuid(), Login = "Ivan", Password = "123", IsBanned = false },
            new PlayerDb() { Id = Guid.NewGuid(), Login = "Petr", Password = "323", IsBanned = false },
            new PlayerDb() { Id = Guid.NewGuid(), Login = "Alex", Password = "153", IsBanned = true },
            new PlayerDb() { Id = Guid.NewGuid(), Login = "Misha", Password = "423", IsBanned = false },
            new PlayerDb() { Id = Guid.NewGuid(), Login = "Evgen", Password = "145", IsBanned = true }
        };

        public static List<PlayerDb> Players { get => _players; }
    }
}
