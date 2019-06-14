using EP.SeaBattle.Logic.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EP.SeaBattle.Logic.Validators
{
    public class PlayerValidation : AbstractValidator<AddNewPlayerCommand>
    {
        public PlayerValidation()
        {
            RuleFor(x => x.NickName.Trim())
                .NotEmpty().WithMessage("NickName cannot be empty")
                .MinimumLength(3).WithMessage("NickName must have 3 characters")
                .MaximumLength(20).WithMessage("NickName must have less 20 characters");

        }
    }

    public class PlayerUpdateValidation : AbstractValidator<UpdatePlayerCommand>
    {
        public PlayerUpdateValidation()
        {
            RuleFor(x => x.NickName.Trim())
                .NotEmpty().WithMessage("NickName cannot be empty")
                .MinimumLength(3).WithMessage("NickName must have 3 characters")
                .MaximumLength(20).WithMessage("NickName must have less 20 characters");

        }
    }
}
