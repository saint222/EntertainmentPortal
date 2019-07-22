using EP.TicTacToe.Logic.Commands;
using FluentValidation;

namespace EP.TicTacToe.Logic.Validators
{
    /// <summary>
    /// 
    /// </summary>
    public class CheckStepValidator : AbstractValidator<AddNewStepCommand>
    {
        /// <summary>
        /// 
        /// </summary>
        public CheckStepValidator()
        {
            RuleFor(x => x.PlayerId)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("Value must be more than 0");
            RuleFor(x => x.GameId)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("Value must be more than 0");

        }
    }
}