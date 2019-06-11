using EP.Balda.Logic.Commands;
using FluentValidation;

namespace EP.Balda.Logic.Validators
{
    public class AddNewPlayerValidator : AbstractValidator<AddNewPlayerCommand>
    {
        public AddNewPlayerValidator()
        {
            RuleFor(x => x.Login)
                .NotEmpty()
                .WithMessage("Login shouldn't be empty")
                .Length(2, 20)
                .WithMessage("Login should contain from 2 to 20 characters");
                
            RuleFor(x => x.NickName)
                .NotEmpty()
                .WithMessage("NickName shouldn't be empty")
                .Length(2, 20)
                .WithMessage("NickName should contain from 2 to 20 characters");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("NickName shouldn't be empty")
                .Length(8, 30);
        }
    }
}
