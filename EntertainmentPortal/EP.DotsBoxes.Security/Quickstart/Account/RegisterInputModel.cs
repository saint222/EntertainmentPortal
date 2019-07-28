using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4.Quickstart.UI
{
    public class RegisterInputModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public bool RememberLogin { get; set; }
        public string ReturnUrl { get; set; }

        public RegisterInputModel()
        {}

        public RegisterInputModel(LoginInputModel model)
        {
            Email = model.Email;
            RememberLogin = false;
            ReturnUrl = model.ReturnUrl;
        }      
    }
}
