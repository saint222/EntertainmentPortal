using System;

namespace EP.SeaBattle.Data.Models
{
    public class PlayerDb
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string NickName { get; set; }
    }
}
