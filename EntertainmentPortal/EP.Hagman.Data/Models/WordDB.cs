using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Hagman.Data.Models
{
    public class WordDB
    {
        public WordDB(string word)
        {
            Name = word;
        }

        public string Name { get; set; }
    }
}
