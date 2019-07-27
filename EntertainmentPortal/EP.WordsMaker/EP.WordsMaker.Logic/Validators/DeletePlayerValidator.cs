using EP.WordsMaker.Logic.Commands;
using FluentValidation;

namespace EP.WordsMaker.Logic.Validators
{
    public class DeletePlayerValidator: AbstractValidator<DeletePlayerCommand>
    {
        public DeletePlayerValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .WithMessage("Value must be more than 0");
        }
    }
}