using EP.Sudoku.Logic.Enums;
using System.Collections.Generic;

namespace EP.Sudoku.Logic.Models
{
    /// <summary>    
    /// Is used to represent an instance of the game.
    /// </summary>
    public class Session
    {
        /// <summary>    
        /// Is used to denote an identification value of a game session.
        /// </summary>
        public int Id { get; set; }        

        /// <summary>    
        /// Is used to denote the level of a game session difficulty (is expected to be chosen by a player).
        /// </summary>
        /// <remarks>
        /// Non-primitive type DifficultyLevel (enum) is used.
        /// </remarks> 
        public DifficultyLevel Level { get; set; }

        /// <summary>    
        /// Is used to denote the number of tips.
        /// </summary>
        public int Hint { get; set; } = 3;

        /// <summary>    
        /// Is used as a flag for the business logic of the Session class.
        /// </summary>
        public bool IsOver { get; set; }        
        
        /// <summary>    
        /// Is used to denote the necessary data, which concernes a player of a game session.
        /// </summary>
        /// <remarks>
        /// Navigation property.
        /// </remarks>        
        public PlayerShort Participant { get; set; }

        /// <summary>
        /// Is used for keeping a strict number (81) of the gameboard parts. 
        /// </summary> 
        public List<Cell> Squares { get; set; }

    }
}
