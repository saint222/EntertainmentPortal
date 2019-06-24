using EP.Balda.Logic.Commands;
using FluentValidation;

namespace EP.Balda.Logic.Validators
{
    /// <summary>
    /// Player deletion validator
    /// </summary>
    public class DeletePlayerValidator : AbstractValidator<DeletePlayerCommand>
    {
        public DeletePlayerValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Value must be more than 0");
        }
    }
}