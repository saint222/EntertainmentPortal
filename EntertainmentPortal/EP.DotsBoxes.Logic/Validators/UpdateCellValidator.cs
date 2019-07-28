using EP.DotsBoxes.Logic.Commands;
using FluentValidation;

namespace EP.DotsBoxes.Logic.Validators
{
    public class UpdateCellValidator : AbstractValidator<UpdateCellCommand>
    {
        public UpdateCellValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            When(x => x.Top, () =>
            {
                RuleFor(x => x.Bottom)
                .NotEqual(true)
                .WithMessage("Only one side can be true");
                RuleFor(x => x.Right)
                .NotEqual(true)
                .WithMessage("Only one side can be true");
                RuleFor(x => x.Left)
                .NotEqual(true)
                .WithMessage("Only one side can be true");
            });

            When(x => x.Bottom, () =>
            {               
                RuleFor(x => x.Right)
                .NotEqual(true)
                .WithMessage("Only one side can be true");
                RuleFor(x => x.Left)
                .NotEqual(true)
                .WithMessage("Only one side can be true");
            });

            When(x => x.Right, () =>
            {                
                RuleFor(x => x.Left)
                .NotEqual(true)
                .WithMessage("Only one side can be true");
            });           
        }
    }
}
