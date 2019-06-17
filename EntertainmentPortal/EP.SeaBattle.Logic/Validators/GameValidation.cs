using EP.SeaBattle.Logic.Commands;
using FluentValidation;

namespace EP.SeaBattle.Logic.Validators
{
    public class GameValidation : AbstractValidator<CreateNewGameCommand>
    {
        public GameValidation()
        {
            RuleSet("GameRule", () =>
            {
                RuleFor(o => o.Player1)
                    .NotNull().WithMessage("Player 1 cannot be null");
                RuleFor(o => o.Player2)
                    .NotNull().WithMessage("Player 2 cannot be null");
                RuleFor(o => o.Player1)
                    .NotEqual(p => p.Player2).WithMessage("Player 1 cannot be equal Player 2");
                RuleFor(o => o.PlayerAllowedToMove)
                    .NotNull().WithMessage("Player allow to move not set");

                RuleFor(o => o.Player1.NickName)
                    .NotEmpty().WithMessage("Cannot create game with player without nickname");
                RuleFor(o => o.Player2.NickName)
                    .NotEmpty().WithMessage("Cannot create game with player without nickname");
            });
        }
    }
}
