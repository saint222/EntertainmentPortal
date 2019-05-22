using System;
using System.Collections.Generic;
using System.Drawing;
using EP.DotsBoxes.Data.Models;

namespace EP.DotsBoxes.Data
{
    public static class PlayerStatistics
    {
        private static List<PlayerDb> _players = new List<PlayerDb>()
        {
            new PlayerDb()
            {
                Id = 123,
                Name = "Vasya",
                Color = Color.Blue,
                Created = DateTime.Now,
                Score = 10
            },

            new PlayerDb()
            {
                Id = 121,
                Name= "Petya",
                Color = Color.Red,
                Created = DateTime.Now,
                Score = 15
            },

            new PlayerDb()
            {
                Id = 111,
                Name = "Igor",
                Color = Color.DarkViolet,
                Created = DateTime.Now,
                Score = 20
            },
        };

        public static List<PlayerDb> Players => _players;
    }
   
}
