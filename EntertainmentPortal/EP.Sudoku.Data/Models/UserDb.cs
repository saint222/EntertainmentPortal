using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Sudoku.Data.Models
{
    public class UserDb
    {
        /// <summary>    
        /// Is used to denote an identification value of a user (DbInfo).
        /// </summary>
        public string Id { get; set; }

        /// <summary>    
        /// Is used to denote the full name of a user (DbInfo).
        /// </summary>
        public string FullName { get; set; }

        /// <summary>    
        /// Is used to denote the email of a user (DbInfo).
        /// </summary>
        public string Email { get; set; }

        /// <summary>    
        /// Is used to denote the login of a user's account (DbInfo).
        /// </summary>
        public string Login { get; set; }

        /// <summary>    
        /// Is used to denote the password of a user's account (DbInfo).
        /// </summary>
        public string Password { get; set; }


        public PlayerDb PlayerDb { get; set; }
    }
}
