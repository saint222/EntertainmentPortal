using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EP._15Puzzle.Data.Context;
using EP._15Puzzle.Logic.Commands;
using EP._15Puzzle.Logic.Queries;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EP._15Puzzle.Logic.Validators
{
    public class MoveTileValidator : AbstractValidator<MoveTileCommand>
    {
        public MoveTileValidator(DeckDbContext context)
        {
            RuleSet("MoveTileSet", () =>
            {
                RuleFor(x => x.Tile)
                    .NotEmpty()
                    .WithMessage("Tile cannot be null")
                    .InclusiveBetween(1, 16)
                    .WithMessage("Tile must be between 1 and 16");

            });
        }
    }
}
