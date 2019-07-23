using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Sudoku.Logic.Models
{
    public class User
    {
        /// <summary>
        /// Is used to denote the user's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>    
        /// Is used to denote the email of a user.
        /// </summary>
        public string Email { get; set; }       

        /// <summary>    
        /// Is used to denote the password of a user's account.
        /// </summary>
        public string Password { get; set; }        
    }
}
