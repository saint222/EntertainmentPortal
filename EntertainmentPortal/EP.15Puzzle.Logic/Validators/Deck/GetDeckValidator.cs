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
    public class GetDeckValidator : AbstractValidator<GetDeckQuery>
    {
        private readonly DeckDbContext _context;
        public GetDeckValidator(DeckDbContext context)
        {
            _context = context;

            RuleSet("IdExistingSet", () =>
            {
                RuleFor(x => x.Id)
                    .MustAsync((o, s, token) => CheckId(o))
                    .WithMessage("There is no user with such Id");
            });
        }

        private async Task<bool> CheckId(GetDeckQuery model)
        {
            var result = await _context.UserDbs.AnyAsync(c => c.Id == model.Id);
            return result;
        }
    }
}
