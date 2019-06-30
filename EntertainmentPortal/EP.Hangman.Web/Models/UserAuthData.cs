namespace EP.Hangman.Web.Models
{
    /// <summary>
    /// Model of user's data for authentication and authorization
    /// </summary>
    public class UserAuthData
    {
        /// <summary>
        /// Property for user's name
        /// </summary>
        public string UserName { get; set; }
        
        /// <summary>
        /// Property for user's password
        /// </summary>
        public string Password { get; set; }

    }
}