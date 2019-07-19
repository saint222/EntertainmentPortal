using EP.TicTacToe.Logic.Commands;
using FluentValidation;

namespace EP.TicTacToe.Logic.Validators
{
    public class AddNewGameValidator : AbstractValidator<AddNewGameCommand>
    {
        public AddNewGameValidator()
        {
            RuleFor(x => x.PlayerOne.ToString())
                .MinimumLength(1)
                .WithMessage("First player id cannot be empty.");
            RuleFor(x => x.PlayerOne.ToString())
                .MinimumLength(1)
                .WithMessage("Second player id cannot be empty.");
            RuleFor(x => x.MapSize)
                .GreaterThan(0)
                .WithMessage("Map size cannot be empty.");
            RuleFor(x => x.MapWinningChain)
                .GreaterThan(0)
                .WithMessage("Winning chain size cannot be empty.");
        }
    }
}