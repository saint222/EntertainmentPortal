using EP.TicTacToe.Logic.Commands;
using FluentValidation;

namespace EP.TicTacToe.Logic.Validators
{
    public class AddNewPlayerValidator : AbstractValidator<AddNewPlayerCommand>
    {
        public AddNewPlayerValidator()
        {
            RuleFor(x => x.NickName)
                .NotEmpty()
                .WithMessage("NickName cannot be null")
                .Length(3, 20)
                .WithMessage("NickName must be from 3 to 20 symbols");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password cannot be null")
                .Length(5, 20)
                .WithMessage("Password must be from 5 to 20 symbols");
        }
    }
}