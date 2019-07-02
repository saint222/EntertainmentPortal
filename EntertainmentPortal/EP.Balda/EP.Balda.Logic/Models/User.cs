namespace EP.Balda.Logic.Models
{
    /// <c>User</c> model class.
    /// Represents the user.
    public class User
    {
        /// <summary>
        /// Login property.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Email property for user's e-mail address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Property for user's password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Property for user's password to confirm.
        /// </summary>
        public string PasswordConfirm { get; set; }
    }
}