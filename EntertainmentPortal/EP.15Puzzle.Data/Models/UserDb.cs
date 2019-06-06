using System;
using System.Collections.Generic;
using System.Text;

namespace EP._15Puzzle.Data.Models
{
    /// <summary>
    /// Represents <c>User</c> class.
    /// </summary>
    public class UserDb
    {
        public int Id { get; set; }

        public DeckDb Deck { get; set; }
        //public int DeckId { get; set; }

        public string Name { get; set; }
        public string Country { get; set; }
        
    }
}
