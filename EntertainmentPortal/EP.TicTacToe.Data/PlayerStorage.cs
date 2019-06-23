using System.Collections.Generic;
using EP.TicTacToe.Data.Models;

namespace EP.TicTacToe.Data
{
    public static class PlayerStorage
    {
        public static List<PlayerDb> _storage = new List<PlayerDb>()
                                                   {
                                                       new PlayerDb()
                                                       {
                                                           Id = 1,
                                                           Age = 18,
                                                           NickName = "CrazyJoe",
                                                           Password = "12345678",
                                                           CountryOfLiving = "Poland"
                                                       },
                                                       new PlayerDb()
                                                       {
                                                           Id = 2,
                                                           Age = 25,
                                                           NickName = "pupsic",
                                                           Password = "1122334455",
                                                           CountryOfLiving = "Belarus"
                                                       },
                                                       new PlayerDb()
                                                       {
                                                           Id = 3,
                                                           Age = 40,
                                                           NickName = "holly",
                                                           Password = "qwerty",
                                                           CountryOfLiving = "Russia"
                                                       },
                                                   };
        public static List<PlayerDb> Players => _storage;
    }
}
