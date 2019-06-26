
namespace EP.TicTacToe.Data.Models
{
    /// <summary>    
    /// Entity class, which specifies all properties of players stored in database.
    /// </summary>
    public class PlayerDb
    {
        /// <summary>    
        /// Property indicates unique Id-number of a player.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>    
        /// Property indicates age of a player.
        /// </summary>
        public int Age { get; set; }

        /// <summary>    
        /// Property indicates unique nickname of a player.
        /// </summary>
        public string NickName { get; set; }

        /// <summary>    
        /// Property indicates password of a player to login.
        /// </summary>
        public string Password { get; set; }

        /// <summary>    
        /// Property indicates country of living of a player.
        /// </summary>
        public string CountryOfLiving { get; set; }
    }
}
