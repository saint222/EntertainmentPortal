using System;
using System.Collections.Generic;

namespace EP.Balda.Data.Models
{
    /// <summary>
    ///     Entity of Players.
    /// </summary>
    public class PlayerDb
    {
        public long Id { get; set; }

        public string NickName { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
        
        public int Score { get; set; }

        public bool IsMoveAllowed { get; set; }

        public long WordId { get; set; }

        public long GameId { get; set; }

        public ICollection<PlayerGameDb> PlayerGames { get; set; }

        public ICollection<PlayerWordDb> PlayerWords { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}