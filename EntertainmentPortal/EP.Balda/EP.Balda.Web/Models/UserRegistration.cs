namespace EP.Balda.Web.Models
{
    /// <summary>
    /// UserRegistration model class.
    /// Represents the user to register.
    /// </summary>
    public class UserRegistration : UserLogin
    {
        /// <summary>
        /// Email property for user's e-mail address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Property for user's password to confirm.
        /// </summary>
        public string PasswordConfirm { get; set; }
    }
}