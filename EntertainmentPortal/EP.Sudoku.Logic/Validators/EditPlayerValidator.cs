using EP.Sudoku.Data.Context;
using EP.Sudoku.Logic.Commands;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace EP.Sudoku.Logic.Validators
{
    public class EditPlayerValidator : AbstractValidator<UpdatePlayerCommand>
    {
        private readonly SudokuDbContext _context;
        private readonly ILogger<PlayerValidator> _logger;
        public EditPlayerValidator(SudokuDbContext context, ILogger<PlayerValidator> logger)
        {
            _context = context;
            _logger = logger;

            RuleSet("PreValidationEditPlayer", () =>
            {
                RuleFor(x => x.player.NickName)
                    .NotEmpty()
                    .WithMessage("NickName must be set up obligatory!")
                    .Length(1, 50)
                    .WithMessage("NickName must contain at least one and can't be longer, than 50 symbols!");
            });

            RuleSet("CheckExistingEditPlayerValidation", () =>
            {
                RuleFor(x => x)
                    .MustAsync(
                        (o, s, token) => CheckExistingPlayer(o)
                    ).WithMessage("A player with the same nickname already exists, change the nickname!");
            });
        }
        private async Task<bool> CheckExistingPlayer(UpdatePlayerCommand model)
        {
            var result = await _context.Players.AnyAsync(c => c.NickName == model.player.NickName)
                .ConfigureAwait(false);
            _logger.LogError("There was an attempt to create a player with the existing nickname.");

            return !result;
        }
    }
}
