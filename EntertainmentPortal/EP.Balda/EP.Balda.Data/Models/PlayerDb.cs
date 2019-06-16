using System;
using System.Collections.Generic;

namespace EP.Balda.Data.Models
{
    /// <summary>
    ///     Entity of Players.
    /// </summary>
    public class PlayerDb
    {
        public int Id { get; set; }

        public string NickName { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
        
        public int Score { get; set; }

        public bool IsMoveAllowed { get; set; }
        
        public List<WordDb> Words { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}