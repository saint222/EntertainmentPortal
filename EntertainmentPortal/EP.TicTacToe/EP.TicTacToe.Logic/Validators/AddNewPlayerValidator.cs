using EP.TicTacToe.Logic.Commands;
using FluentValidation;

namespace EP.TicTacToe.Logic.Validators
{
    public class AddNewPlayerValidator : AbstractValidator<AddNewPlayerCommand>
    {
        public AddNewPlayerValidator()
        {
            //RuleFor(x => x.UserName)
            //    .NotEmpty()
            //    .WithMessage("UserName cannot be null")
            //    .Length(3, 20)
            //    .WithMessage("UserName must be from 3 to 20 symbols");

            //RuleFor(x => x.Password)
            //    .NotEmpty()
            //    .WithMessage("Password cannot be null")
            //    .Length(8, 20)
            //    .WithMessage("Password must be from 8 to 20 symbols");
        }
    }
}