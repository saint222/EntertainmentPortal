using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using EP.Hagman.Data.Models;
using Bogus;

namespace EP.Hagman.Data
{
    public class HangmanWordsData
    {
        private Faker<WordData> _faker = new Faker<WordData>();
        
        public HangmanWordsData()
        {
            _faker.RuleFor(prop => prop.Name, set => set.Lorem.Word());
        }

        public List<WordData> AllWords => _faker.Generate(15);
    }
}
