using EP.Balda.Data.Context;
using EP.Balda.Web.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EP.Balda.Web.Validators
{
    public class UserRegistrationValidator : AbstractValidator<UserRegistration>
    {
        private readonly BaldaGameDbContext _context;

        public UserRegistrationValidator(BaldaGameDbContext context)
        {
            _context = context;

            RuleFor(x => x.UserName).Length(1,15)
                .WithMessage("UserName should be more than 1 and less than 15 symbols");
            RuleFor(x => x.Password).MinimumLength(1)
                .WithMessage("Password shouldn't be empty");
            RuleFor(x => x.Email).EmailAddress()
                .WithMessage("E-mail is incorrect");
            RuleFor(x => x)
                .Must(
                    (o) => CheckPasswordsMatch(o)
                        ).WithMessage("Passwords do not match");
            RuleFor(x => x)
                    .MustAsync(
                        (o, s, token) => CheckExistingUserName(o)
                           ).WithMessage("User with such name already exists");
            RuleFor(x => x)
                    .MustAsync(
                        (o, s, token) => CheckExistingEmail(o)
                           ).WithMessage("User with such e-mail already exists");
        }

        private bool CheckPasswordsMatch(UserRegistration model)
        {
            return model.Password == model.PasswordConfirm ? true : false;
        }

        private async Task<bool> CheckExistingUserName(UserRegistration model)
        {
            var result = await _context.Users.AnyAsync(c => c.UserName == model.UserName);

            return !result;
        }

        private async Task<bool> CheckExistingEmail(UserRegistration model)
        {
            var result = await _context.Users.AnyAsync(c => c.Email == model.Email);
                
            return !result;
        }
    }
}
