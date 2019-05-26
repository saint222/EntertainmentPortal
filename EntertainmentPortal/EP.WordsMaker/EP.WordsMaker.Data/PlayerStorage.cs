using System;
using System.Collections.Generic;
using System.Text;

namespace EP.WordsMaker.Data
{
    public class PlayerStorage
    {
        private static List<PlayerDb> _storage = new List<PlayerDb>()
                                                 {
                                                    new PlayerDb()
                                                    {
                                                        Id = 1,
                                                        Name = "123123",
                                                        Score = 123,
                                                        LastGame = DateTime.Now
                                                    },

                                                    new PlayerDb()
                                                    {
                                                        Id = 2,
                                                        Name = "jfksd",
                                                        Score = 321,
                                                        LastGame = DateTime.Now
                                                    },

                                                    new PlayerDb()
                                                    {
                                                        Id = 3,
                                                        Name = "swqe",
                                                        Score = 1023,
                                                        LastGame = DateTime.Now
                                                    }
                                                 };

        public static List<PlayerDb> Players => _storage;
    }
}
