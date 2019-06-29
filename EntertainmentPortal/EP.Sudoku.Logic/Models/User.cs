using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Sudoku.Logic.Models
{
    public class User
    {
        /// <summary>    
        /// Is used to denote an identification value of a user.
        /// </summary>
        public string Id { get; set; }

        /// <summary>    
        /// Is used to denote the full name of a user.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>    
        /// Is used to denote the email of a user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>    
        /// Is used to denote the login of a user's account.
        /// </summary>
        public string Login { get; set; }

        /// <summary>    
        /// Is used to denote the password of a user's account.
        /// </summary>
        public string Password { get; set; }   
        
        
        public Player Player { get; set; }
    }
}
