using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.Sudoku.Logic.Models
{
    //if it is necessary...
    public class Player
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        /// <summary>
        /// convertation of game-sessions duration
        /// </summary>
        public int ExperiencePoint { get; set; }
        /// <summary>
        /// player-progression feature due to his/her the sum of experience points
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// strict quantity icons of icons for choosing is meant
        /// </summary>
        public AvatarIcon AvatarIcon { get; set; }
    }
}
