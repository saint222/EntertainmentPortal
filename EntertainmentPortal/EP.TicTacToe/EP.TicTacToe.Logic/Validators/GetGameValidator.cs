using EP.TicTacToe.Logic.Queries;
using FluentValidation;

namespace EP.TicTacToe.Logic.Validators
{
    public class GetGameValidator : AbstractValidator<GetGame>
    {
        public GetGameValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Value must be more than 0");
        }
    }
}