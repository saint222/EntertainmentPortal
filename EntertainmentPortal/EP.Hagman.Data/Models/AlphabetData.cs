using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Hagman.Data.Models
{
    public class AlphabetData
    {
        public AlphabetData()
        {
            Alphabet = new List<string>();
        }
        public List<string> Alphabet { get; set; }
    }
}
