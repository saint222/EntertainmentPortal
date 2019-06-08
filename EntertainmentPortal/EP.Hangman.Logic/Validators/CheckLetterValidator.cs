using EP.Hangman.Logic.Commands;
using FluentValidation;

namespace EP.Hangman.Logic.Validators
{
    public class CheckLetterValidator : AbstractValidator<CheckLetterCommand>
    {
        public CheckLetterValidator()
        {
            RuleFor(x => x._data.Id)
                .GreaterThan(0);
            RuleFor(x => x._data.Letter)
                .NotEmpty()
                .Length(1)
                .WithMessage("Must be One Letter")
                .Matches("[A-Z]")
                .WithMessage("Only uppercase letters");

        }
    }
}
