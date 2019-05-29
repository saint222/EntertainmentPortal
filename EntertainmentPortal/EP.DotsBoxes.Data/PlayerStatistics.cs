using System;
using System.Collections.Generic;
using System.Drawing;
using Bogus;
using EP.DotsBoxes.Data.Models;

namespace EP.DotsBoxes.Data
{
    /// <summary>
    /// The PlayerStatistics class creates and stores a list of players with all the data.
    /// </summary>
    public static class PlayerStatistics
    {
        private static Faker<PlayerDb> _faker = new Faker<PlayerDb>();

        static PlayerStatistics()
        {
            _faker.RuleFor(x => x.Id, f => f.IndexFaker)
                .RuleFor(x => x.Name, f => f.Name.FirstName())
                .RuleFor(x => x.Color, f => f.Commerce.Color())
                .RuleFor(x => x.Score, f => f.Random.Int(0,30));
        }

        public static List<PlayerDb> Players => _faker.Generate(30);
    }
   
}
