using EP.Balda.Web.Models;
using FluentValidation;

namespace EP.Balda.Web.Validators
{
    public class UserLoginValidator : AbstractValidator<UserLogin>
    {
        public UserLoginValidator()
        {
            RuleFor(x => x.UserName).MinimumLength(1)
                .WithMessage("UserName shouldn't be empty");
            RuleFor(x => x.Password).MinimumLength(1)
                .WithMessage("Password shouldn't be empty");
        }
    }
}
