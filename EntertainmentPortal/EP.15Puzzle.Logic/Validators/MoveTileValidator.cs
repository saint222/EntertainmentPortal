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
        private readonly DeckDbContext _context;

        public MoveTileValidator(DeckDbContext context)
        {
            _context = context;


            RuleSet("MoveTileSet", () =>
            {
                RuleFor(x => x.Tile)
                    .NotEmpty()
                    .WithMessage("Tile cannot be null")
                    .InclusiveBetween(1, 16)
                    .WithMessage("Tile must be between 1..size*size");

                RuleFor(x => x.Id)
                    .MustAsync((o, s, token) => CheckId(o))
                    .WithMessage("There is no user with such Id");
            });
        }

        private async Task<bool> CheckId(MoveTileCommand model)
        {
            var result = await _context.UserDbs.AnyAsync(c => c.Id == model.Id);
            return result;
        }
    }
}
