using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Sudoku.Data.Models
{
    /// <summary>    
    /// Is used to represent an instance of the game (DbInfo).
    /// </summary>
    public class SessionDb
    {
        /// <summary>    
        /// Is used to denote an identification value of a game session (DbInfo).
        /// </summary>
        public long Id { get; set; }

        /// <summary>    
        /// Is used to denote the level of a game session difficulty (is expected to be chosen by a player) (DbInfo).
        /// </summary>
        /// <remarks>
        /// Non-primitive type DifficultyLevel (enum) is used (DbInfo).
        /// </remarks>         
        public int Level { get; set; }

        /// <summary>    
        /// Represents the possibility to get three automatically set values durring the game as prompts (DbInfo).
        /// </summary>
        public int Hint { get; set; } = 3;

        /// <summary>    
        /// Is used as a flag for the business logic of the Session class (DbInfo).
        /// </summary>
        public bool IsOver { get; set; }        
        
        /// <summary>    
        /// Is used to denote the necessary data, which concernes a player of a game session (DbInfo).
        /// </summary>
        /// <remarks>
        /// Navigation property.
        /// </remarks>
        //public PlayerDb ParticipantDb { get; set; }

        /// <summary>
        /// Is used for keeping a strict number (81) of the gameboard parts (DbInfo). 
        /// </summary> 
        public List<CellDb> SquaresDb { get; set; }


        public string PlayerDbId { get; set; }
    }
}
