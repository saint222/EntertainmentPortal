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
        //private static List<WordData> _wordsStorage = new List<WordData>
        //{
        //    {new WordData("angry")},
        //    {new WordData("fascinating")},
        //    {new WordData("wonderful")},
        //    {new WordData("environment")},
        //    {new WordData("zombie")},
        //    {new WordData("neighbour")},
        //    {new WordData("investigate")},
        //    {new WordData("mistake")},
        //    {new WordData("nature")},
        //};

        public List<WordData> AllWords => _faker.Generate(15);

        //public void AddWord(string word)
        //{
        //    _wordsStorage.Add(new WordData(word));
        //}
    }
}
