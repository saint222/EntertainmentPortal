using EP.Balda.Data.Context;
using EP.Balda.Logic.Commands;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EP.Balda.Logic.Validators
{
    /// <summary>
    ///     Adding letter to cell validator.
    /// </summary>
    public class AddLetterToCellValidator : AbstractValidator<AddLetterToCellCommand>
    {
        private readonly BaldaGameDbContext _context;

        public AddLetterToCellValidator(BaldaGameDbContext context)
        {
            _context = context;

            RuleSet("CellExistingSet", () =>
            {
                RuleFor(x => x)
                    .MustAsync(
                        (o, s, token) => CheckExistingCell(o)
                           ).WithMessage("Cell doesn't exist");
            });

            RuleSet("AddLetterToCellPreValidation", () =>
            {
                RuleFor(x => x.Id)
                    .GreaterThan(0)
                    .WithMessage("Id must be more than 0");

                RuleFor(x => x.Letter)
                    .NotNull()
                    .WithMessage("Letter shouldn't be null");
            });
        }

        private async Task<bool> CheckExistingCell(AddLetterToCellCommand model)
        {
            var result = await _context.Cells
                .Where(c => c.Id == model.Id)
                .FirstOrDefaultAsync();

            return result == null ? false : true;
        }
    }
}
