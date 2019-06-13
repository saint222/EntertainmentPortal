using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Sudoku.Logic.Models
{
    public class Player : PlayerShort
    {
        /// <summary>
        /// Is used to provide a player with a possibility to have more than one unfinished game.
        /// </summary> 
        public List<Session> GameSessions { get; set; }
    }
}
