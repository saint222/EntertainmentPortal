namespace EP.Sudoku.Data.Models
{
    /// <summary>    
    /// Is used to represent an icon for a player's profile (DbInfo).
    /// </summary>
    public class AvatarIconDb
    {
        /// <summary>    
        /// Is used to denote an identification value of a player's avatar picture (DbInfo).
        /// </summary>
        public long Id { get; set; }

        /// <summary>    
        /// Is used to denote URI of a player's avatar picture (DbInfo).
        /// </summary>
        public string Uri { get; set; }

        /// <summary>    
        /// Is used as a flag for setting up the default value of player's avatar picture (DbInfo).
        /// </summary>
        public bool IsBaseIcon { get; set; }
    }
}