namespace EP.SeaBattle.Web
{
    /// <summary>
    /// Model of user's data for authentication and authorization
    /// </summary>
    public class AuthData
    {
        /// <summary>
        /// Property for user's name
        /// </summary>
        public string UserName { get; set; }
        
        /// <summary>
        /// Property for user's password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Property for user's email
        /// </summary>
        public string Email { get; set; }

    }
}