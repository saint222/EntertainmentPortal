
namespace EP.TicTacToe.Logic.Models
{
    /// <summary>    
    /// Class, which specifies all properties of players in a current game.
    /// </summary>
    public class Player
    {
        /// <summary>    
        /// Property indicates unique Id-number of a player in a current game.
        /// </summary>
        public int Id { get; set; }

        /// <summary>    
        /// Property indicates unique nickname of a player in a current game.
        /// </summary>
        public string NickName { get; set; }

        /// <summary>    
        /// Property indicates password of a player in a current game.
        /// </summary>
        public string Password { get; set; }
    }
}
