using System.ComponentModel.DataAnnotations;

namespace IdentityServer4.Quickstart.UI
{
    public class RegisterInputModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public bool RememberLogin { get; set; }
        public string ReturnUrl { get; set; }

        public RegisterInputModel(LoginInputModel model)
        {
            Email = model.Email;
            RememberLogin = false;
            ReturnUrl = model.ReturnUrl;
        }

        public RegisterInputModel()
        {

        }
    }
}