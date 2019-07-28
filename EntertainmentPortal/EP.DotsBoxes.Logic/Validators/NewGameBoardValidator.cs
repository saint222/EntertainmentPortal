using EP.DotsBoxes.Logic.Commands;
using FluentValidation;

namespace EP.DotsBoxes.Logic.Validators
{
    public class NewGameBoardValidator : AbstractValidator<NewGameBoardCommand>
    {
        public NewGameBoardValidator()
        {
            RuleFor(x => x.Columns)
                .GreaterThan(2)
                .WithMessage("Value must be more than 2")
                .LessThan(11)
                .WithMessage("Value must be less than 11");

            RuleFor(x => x.Rows)
                .GreaterThan(2)
                .WithMessage("Value must be more than 2")
                .LessThan(11)
                .WithMessage("Value must be less than 11");
        }
    }
}
