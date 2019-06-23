using EP.Sudoku.Data.Context;
using EP.Sudoku.Logic.Commands;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Sudoku.Logic.Validators
{
    public class PlayerValidator : AbstractValidator<CreatePlayerCommand>
    {     
        public PlayerValidator()
        {                 
            RuleSet("PreValidationPlayer", () =>
            {
                RuleFor(x => x.player.NickName)
                    .NotEmpty()                    
                    .WithMessage("NickName must be set up obligatory!")
                    .Length(1, 50)
                    .WithMessage("NickName must contain at least one and can't be longer, than 50 symbols!");                
            });            
        }
    }
}
