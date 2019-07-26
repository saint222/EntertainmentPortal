using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using EP.WordsMaker.Data.Models;

namespace EP.WordsMaker.Data
{
    public class PlayerStorage
    {
        private static Faker<PlayerDb> _faker = new Faker<PlayerDb>();

        static PlayerStorage()
        {
            _faker.RuleFor(p => p.Id, f => f.IndexFaker.ToString()).
                RuleFor(p => p.Name, f => f.Random.Word()).
                RuleFor(p => p.BestScore, f => f.Random.Int(100, 1000)).
                RuleFor(p => p.LastGame, f => f.Date.Past(1));
        }

        public static List<PlayerDb> Players => _faker.Generate(10);
    }
}
