namespace EP.Balda.Data.Models
{
    /// <summary>
    ///     Entity of players
    /// </summary>
    public class PlayerDb
    {
        public int Id { get; set; }

        public string NickName { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        /// <summary>
        ///     player achievements in points
        /// </summary>
        public int Score { get; set; }
    }
}