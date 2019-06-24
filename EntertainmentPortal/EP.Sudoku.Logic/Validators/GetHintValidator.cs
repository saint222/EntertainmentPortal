using System.Threading.Tasks;
using EP.Sudoku.Data.Context;
using EP.Sudoku.Data.Models;
using EP.Sudoku.Logic.Commands;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EP.Sudoku.Logic.Validators
{
    public class GetHintValidator : AbstractValidator<GetHintCommand>
    {
        private readonly SudokuDbContext _context;

        public GetHintValidator(SudokuDbContext context)
        {
            _context = context;

            RuleSet("IsValidGetHint", () =>
            {
                RuleFor(x => x)
                    .Must((o, token) =>
                    {
                        var session = GetSession(o);

                        return session.Result.Hint > 0 ? true : false;
                    })
                    .WithMessage($"Hint are over");
            });
        }

        private async Task<SessionDb> GetSession(GetHintCommand model)
        {
            var session = await _context.Sessions
                .Include(d => d.SquaresDb)
                .FirstAsync(d => d.Id == model.SessionId);

            return session;
        }
    }
}
