using EP.WordsMaker.Logic.Commands;
using FluentValidation;

namespace EP.WordsMaker.Logic.Validators
{
    public class AddNewPlayerValidator : AbstractValidator<AddNewPlayerCommand>
    {
        public AddNewPlayerValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Title cannot be null")
                .Length(3, 50)
                .WithMessage("Name must be more than 3 and less than 50");
        }
    }
}