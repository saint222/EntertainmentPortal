using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Hagman.Data.Models
{
    public class WordData
    {
        public WordData(string word)
        {
            Name = word;
        }

        public string Name { get; set; }
    }
}
