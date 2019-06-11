using EP.Hangman.Logic.Queries;
using FluentValidation;

namespace EP.Hangman.Logic.Validators
{
    public class GetUserSessionValidator : AbstractValidator<GetUserSession>
    {
        public GetUserSessionValidator()
        {
            RuleFor(x => x._data.Id)
                .GreaterThan(0)
                .WithMessage("Value must be more than 0");
            RuleFor(x => x._data.Alphabet)
                .Null()
                .WithMessage("Value Alphabet must be null");
            RuleFor(x => x._data.CorrectLetters)
                .Null()
                .WithMessage("Value CorrectLetters must be null");
            RuleFor(x => x._data.Letter)
                .Null()
                .WithMessage("Value Letter must be null");
            RuleFor(x => x._data.UserErrors)
                .Equal(0)
                .WithMessage("Value UserErrors must be 0");
        }
    }
}
