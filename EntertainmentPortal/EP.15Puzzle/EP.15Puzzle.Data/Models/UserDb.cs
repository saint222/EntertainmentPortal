using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace EP._15Puzzle.Data.Models
{
    /// <summary>
    /// Represents <c>User</c> class.
    /// </summary>
    public class UserDb : IdentityUser
    {
        public string Sub { get; set; }

        public DeckDb Deck { get; set; }
        public ICollection<RecordDb> Records { get; set; } = new List<RecordDb>();
    }
}
