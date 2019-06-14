using System;
using System.Collections.Generic;
using System.Text;
using EP.SeaBattle.Logic.Commands;
using FluentValidation;

namespace EP.SeaBattle.Logic.Validators
{
    public class ShipValidation : AbstractValidator<AddNewCellCommand>
    {
        public ShipValidation()
        {
            RuleFor(obj => obj.X).InclusiveBetween((byte)0, (byte)9).WithMessage("X must be between 0 and 9");
            RuleFor(obj => obj.Y).InclusiveBetween((byte)0, (byte)9).WithMessage("Y must be between 0 and 9");
            RuleFor(obj => obj.Status).IsInEnum().WithMessage("Status not correct");
        }
    }
}
