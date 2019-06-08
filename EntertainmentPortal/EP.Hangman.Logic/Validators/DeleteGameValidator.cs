using EP.Hangman.Logic.Commands;
using FluentValidation;

namespace EP.Hangman.Logic.Validators
{
    public class DeleteGameValidator : AbstractValidator<DeleteGameSessionCommand>
    {
        public DeleteGameValidator()
        {
            RuleFor(x => x._data.Id)
                .GreaterThan(0)
                .WithMessage("Value must be more than 0");
        }
    }
}
