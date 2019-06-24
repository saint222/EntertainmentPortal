using System.Threading.Tasks;
using EP.Balda.Data.Context;
using EP.Balda.Logic.Commands;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EP.Balda.Logic.Validators
{
    /// <summary>
    ///     Player creation validator.
    /// </summary>
    public class CreateNewPlayerValidator : AbstractValidator<CreateNewPlayerCommand>
    {
        private readonly BaldaGameDbContext _context;

        public CreateNewPlayerValidator(BaldaGameDbContext context)
        {
            _context = context;

            RuleSet("PlayerExistingSet", () =>
            {
                RuleFor(x => x)
                    .MustAsync(
                        (o, s, token) => CheckExistingPlayer(o)
                    ).WithMessage("Player already exists.");
            });

            RuleSet("PlayerPreValidation", () =>
            {
                RuleFor(x => x.Login)
                    .NotEmpty()
                    .WithMessage("Login cannot be null.")
                    .Length(1, 50)
                    .WithMessage("Login must be more than empty.");

                RuleFor(x => x.Password)
                    .NotEmpty()
                    .WithMessage("Password cannot be null.")
                    .Length(4, 8)
                    .WithMessage(
                        "Password must be more than 4 and less than 9 characters.");

                RuleFor(x => x.NickName)
                    .NotEmpty()
                    .WithMessage("NickName cannot be null.")
                    .Length(1, 50)
                    .WithMessage("NickName must be more than empty.");
            });
        }

        private async Task<bool> CheckExistingPlayer(CreateNewPlayerCommand model)
        {
            var result = await _context.Players.AnyAsync(c => c.Login == model.Login)
                .ConfigureAwait(false);

            return !result;
        }
    }
}