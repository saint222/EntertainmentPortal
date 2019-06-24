using System.Threading.Tasks;
using EP.Balda.Data.Context;
using EP.Balda.Logic.Commands;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EP.Balda.Logic.Validators
{
    /// <summary>
    ///     Player deletion validator.
    /// </summary>
    public class DeletePlayerValidator : AbstractValidator<DeletePlayerCommand>
    {
        private readonly BaldaGameDbContext _context;

        public DeletePlayerValidator(BaldaGameDbContext context)
        {
            _context = context;

            RuleSet("PlayerDbNotExistingSet", () =>
            {
                RuleFor(x => x)
                    .MustAsync(
                        (o, s, token) => CheckForNonExistingPlayerAsync(o)
                    ).WithMessage("No such player exists.");
            });

            RuleSet("PlayerDelPreValidation", () =>
            {
                RuleFor(x => x.Id)
                    .NotEmpty()
                    .GreaterThan(0)
                    .WithMessage("Value Id must be more than 0.");
            });
        }

        private async Task<bool> CheckForNonExistingPlayerAsync(DeletePlayerCommand model)
        {
            var result = await _context.Players.AnyAsync(c => c.Id == model.Id)
                .ConfigureAwait(false);

            return result;
        }
    }
}