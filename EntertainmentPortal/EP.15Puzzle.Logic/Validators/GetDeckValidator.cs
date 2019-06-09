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
    public class GetDeckValidator : AbstractValidator<GetDeckQuery>
    {
        public GetDeckValidator(DeckDbContext context)
        {
            
            RuleFor(x => x.Id)
                .MustAsync(
                    async (o, s, token) =>
                        await context.UserDbs.AnyAsync(c => c.Id == o.Id))
                .WithMessage("There is no user with such Id");

        }
    }
}
