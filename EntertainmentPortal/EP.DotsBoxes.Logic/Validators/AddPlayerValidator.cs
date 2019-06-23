using EP.DotsBoxes.Logic.Commands;
using FluentValidation;

namespace EP.DotsBoxes.Logic.Validators
{
    public class AddPlayerValidator : AbstractValidator<AddPlayerCommand>
    {
        public AddPlayerValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name shouldn't be empty")
                .Length(2, 30)
                .WithMessage("NickName should contain from 2 to 30 characters");

            RuleFor(x => x.Color)
                .NotEmpty()
                .WithMessage("Color shouldn't be empty")
                .Length(2, 30);
        }
    }
}
