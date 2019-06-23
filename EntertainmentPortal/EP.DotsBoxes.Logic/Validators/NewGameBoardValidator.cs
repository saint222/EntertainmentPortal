using EP.DotsBoxes.Logic.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EP.DotsBoxes.Logic.Validators
{
    public class NewGameBoardValidator : AbstractValidator<NewGameBoardCommand>
    {
        public NewGameBoardValidator()
        {
            RuleFor(x => x.Columns)
                .GreaterThan(0)
                .WithMessage("Value must be more than 0");

            RuleFor(x => x.Rows)
                .GreaterThan(0)
                .WithMessage("Value must be more than 0");
        }
    }
}
