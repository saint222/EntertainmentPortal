using System.Collections.Generic;
using EP._15Puzzle.Data.Models;

namespace EP._15Puzzle.Logic.Models
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public ICollection<RecordDb> Records { get; set; } = new List<RecordDb>();
    }
}
