using System.Collections.Generic;
using EP._15Puzzle.Data.Models;

namespace EP._15Puzzle.Logic.Models
{
    public class UserCred
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
        public string AuthType { get; set; }
        public string AuthId { get; set; }
    }
}
