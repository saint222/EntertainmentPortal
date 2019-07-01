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
        }
    }
}
