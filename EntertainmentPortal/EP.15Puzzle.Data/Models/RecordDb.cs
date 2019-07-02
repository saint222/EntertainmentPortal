using System;
using System.Collections.Generic;
using System.Text;

namespace EP._15Puzzle.Data.Models
{
    public class RecordDb
    {
        public int Id { get; set; }
        public UserDb User { get; set; }
        public string UserId { get; set; }
        public int Score { get; set; }
    }
}
