using EP.SeaBattle.Data.Context;
using EP.SeaBattle.Logic.Commands;
using FluentValidation;
using System.Threading.Tasks;

namespace EP.SeaBattle.Logic.Validators
{
    //TODO объявить названия валидаций как константы и использовать константы
    public class GameValidation : AbstractValidator<CreateNewGameCommand>
    {
        SeaBattleDbContext _context;
        public GameValidation(SeaBattleDbContext context)
        {
            _context = context;
            RuleSet("GamePreValidation", () =>
            {
                RuleFor(o => o.PlayerId)
                    .NotEmpty().WithMessage("Player 1 cannot be null");
            });

            RuleSet("GameValidation", () =>
            {
                RuleFor(x => x)
                    .MustAsync((o, s, token) => CheckExistingPlayer(o.PlayerId))
                        .WithMessage(player => $"Player1 with id {player.PlayerId} doesn't exists");

            });
        }

        private async Task<bool> CheckExistingPlayer(string id)
        {
            var result = await _context.Players.FindAsync(id)
                .ConfigureAwait(false);
            if (result == null)
                return false;
            return true;
        }
    }
}
