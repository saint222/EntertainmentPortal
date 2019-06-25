using EP.Balda.Data.Context;
using EP.Balda.Logic.Queries;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EP.Balda.Logic.Validators
{
    /// <summary>
    ///     Get player validator.
    /// </summary>
    public class GetPlayerValidator : AbstractValidator<GetPlayer>
    {
        private readonly BaldaGameDbContext _context;

        public GetPlayerValidator(BaldaGameDbContext context)
        {
            _context = context;

            RuleSet("PlayerExistingSet", () =>
            {
                RuleFor(x => x)
                    .MustAsync(
                        (o, s, token) => CheckExistingPlayer(o)
                           ).WithMessage("Player doesn't exist");
            });

            RuleSet("GetPlayerPreValidation", () =>
            {
                RuleFor(x => x.Id)
                    .GreaterThan(0)
                    .WithMessage("Id must be more than 0");
            });
        }

        private async Task<bool> CheckExistingPlayer(GetPlayer model)
        {
            var result = await _context.Players
                .Where(c => c.Id == model.Id)
                .FirstOrDefaultAsync();

            return result == null ? false : true;
        }
    }
}
