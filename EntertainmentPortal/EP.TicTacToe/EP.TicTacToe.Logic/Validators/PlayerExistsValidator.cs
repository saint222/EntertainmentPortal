using EP.TicTacToe.Data.Context;
using EP.TicTacToe.Logic.Commands;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EP.TicTacToe.Logic.Validators
{
    public class PlayerExistsValidator : AbstractValidator<AddNewPlayerCommand>
    {
        public PlayerExistsValidator(TicTacDbContext context)
        {
            RuleSet("PlayerExistingSet", () =>
            {
                //RuleFor(x => x.UserName)
                //.MustAsync(
                //    async (o, s, token) =>
                //    await context.Player.AnyAsync(c => c.UserName == s));
            });
        }
    }
}