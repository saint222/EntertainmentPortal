namespace EP.Balda.Data.Models
{
    /// <summary>
    ///     Table to form many-to-many relations.
    /// </summary>
    public class PlayerGameDb
    {
        public long PlayerId { get; set; }

        public PlayerDb Player { get; set; }

        public long GameId { get; set; }

        public GameDb Game { get; set; }
    }
}