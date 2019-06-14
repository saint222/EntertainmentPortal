using EP.Hangman.Logic.Commands;
using FluentValidation;

namespace EP.Hangman.Logic.Validators
{
    public class CheckLetterValidator : AbstractValidator<CheckLetterCommand>
    {
        public CheckLetterValidator()
        {
            RuleFor(x => x._data.Id)
                .GreaterThan(0)
                .WithMessage("Value must be more than 0");
            RuleFor(x => x._data.Letter)
                .NotEmpty()
                .Length(1)
                .Matches("[A-Z]")
                .WithMessage("Value must be one uppercase letter");
            RuleFor(x => x._data.Alphabet)
                .Null()
                .WithMessage("Value Alphabet must be null");
            RuleFor(x => x._data.CorrectLetters)
                .Null()
                .WithMessage("Value CorrectLetters must be null");
            RuleFor(x => x._data.UserErrors)
                .Equal(0)
                .WithMessage("Value UserErrors must be 0");
        }
    }
}
