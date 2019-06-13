using EP.SeaBattle.Logic.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EP.SeaBattle.Logic.Validators
{
    public class AddNewPlayerValidation : AbstractValidator<AddNewPlayerCommand>
    {
        public AddNewPlayerValidation()
        {
            RuleFor(x => x.NickName.Trim()).NotEmpty().WithMessage("NickName cannot be empty");
            RuleFor(x => x.CanShoot).NotNull();
        }
    }
}
