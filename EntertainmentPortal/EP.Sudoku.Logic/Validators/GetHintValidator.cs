using System.Linq;
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

            RuleSet("PreValidationGetHint", () =>
            {
                RuleFor(x => x)
                    .Must(p => p.Id > 0)
                    .WithMessage("Id must be greater than 0!");
            });

            RuleSet("IsValidGetHint", () =>
            {
                RuleFor(x => x)
                    .Must((o, token) =>
                    {
                        var session = GetSession(o);
                        var value = session.Result.SquaresDb.First(x => x.Id == o.Id).Value;
                        return value == 0 ? true : false;
                    })
                    .WithMessage($"The value is already there")
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
