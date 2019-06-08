using System;
using System.Collections.Generic;
using System.Text;
using EP._15Puzzle.Data.Context;
using EP._15Puzzle.Logic.Commands;
using EP._15Puzzle.Logic.Queries;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EP._15Puzzle.Logic.Validators
{
    public class MoveTileValidator : AbstractValidator<MoveTileCommand>
    {
        public MoveTileValidator()
        {
            RuleFor(x => x.Tile)
                .NotEmpty()
                .WithMessage("Tile cannot be null")
                .InclusiveBetween(1,16)
                .WithMessage("Tile must be between 1..16");
        }

        public MoveTileValidator(DeckDbContext context)
        {
            RuleFor(x => x.Tile)
                .NotEmpty()
                .WithMessage("Tile cannot be null")
                .InclusiveBetween(1, 16)
                .WithMessage("Tile must be between 1..16");
            
            RuleFor(x => x.Id)
                .MustAsync(
                    async (o, s, token) =>
                        await context.UserDbs.AnyAsync(c => c.Id == o.Id))
                .WithMessage("There is no user with such Id");
                
        }
    }
}
