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

            RuleSet("PlayerCreateExistingSet", () =>
            {
                RuleFor(x => x)
                    .MustAsync(
                        (o, s, token) => CheckExistingPlayer(o)
                    ).WithMessage("Another player already exists in the system with the same login name");
            });

            RuleSet("CreatePlayerPreValidation", () =>
            {
                RuleFor(x => x.Login)
                    .NotEmpty()
                    .WithMessage("Login shouldn't be empty")
                    .Length(1, 50)
                    .WithMessage("Login shouldn't be empty");

                RuleFor(x => x.Password)
                    .NotEmpty()
                    .WithMessage("Password shouldn't be empty")
                    .Length(4, 8)
                    .WithMessage(
                        "Password must be more than 4 and less than 9 characters.");

                RuleFor(x => x.NickName)
                    .NotEmpty()
                    .WithMessage("NickName shouldn't be empty.")
                    .Length(1, 50)
                    .WithMessage("NickName shouldn't be empty.");
            });
        }

        private async Task<bool> CheckExistingPlayer(CreateNewPlayerCommand model)
        {
            var result = await _context.Players.
                AnyAsync(c => c.Login == model.Login);

            return !result;
        }
    }
}