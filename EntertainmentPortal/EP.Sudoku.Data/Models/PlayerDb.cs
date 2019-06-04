using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Sudoku.Data.Models
{
    /// <summary>    
    /// Is used to represent a player of the game (DbInfo).
    /// </summary>
    public class PlayerDb
    {
        /// <summary>    
        /// Is used to denote an identification value of a player (DbInfo).
        /// </summary>
        public int Id { get; set; }

        /// <summary>    
        /// Is used to denote the nickname of a player (DbInfo).
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// Is used to denote convertation of gamesessions duration (DbInfo).
        /// </summary>
        public int ExperiencePoint { get; set; }

        /// <summary>
        /// Is used to denote player's progression feature due to his/her sum of experience points (DbInfo).
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Is used to denote strict quantity of icons for choosing (DbInfo).
        /// </summary>
        /// <remarks>
        /// The non-primitive type AvatarIcon is applied.
        /// </remarks>
        public virtual AvatarIconDb IconDb { get; set; }
    }
}
