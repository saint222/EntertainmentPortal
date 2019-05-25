using System;

namespace EP.WordsMaker.Data
{
    public class PlayerDb
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Score { get; set; }

        public DateTime LastGame { get; set; }
    }
}
