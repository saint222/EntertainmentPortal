using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Hagman.Data.Models
{
    /// <summary>
    /// Represents collection that stores all letters, available for input
    /// </summary>
    public class AlphabetData
    {
        public AlphabetData()
        {
            Alphabet = new List<string>();
        }
        public List<string> Alphabet { get; set; }
    }
}
