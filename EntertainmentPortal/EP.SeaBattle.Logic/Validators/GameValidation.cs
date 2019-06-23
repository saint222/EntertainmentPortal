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
                RuleFor(o => o.Player1Id)
                    .NotEmpty().WithMessage("Player 1 cannot be null");
                RuleFor(o => o.Player2Id)
                    .NotEmpty().WithMessage("Player 2 cannot be null");
                RuleFor(o => o.Player1Id)
                    .NotEqual(p => p.Player2Id).WithMessage("Player 1 cannot be equal Player 2");
                RuleFor(o => o.PlayerAllowedToMoveId)
                    .NotEmpty().WithMessage("Player allow to move not set");
            });

            RuleSet("GameValidation", () =>
            {
                RuleFor(x => x)
                    .MustAsync((o, s, token) => CheckExistingPlayer(o.Player1Id))
                        .WithMessage(player => $"Player1 with id {player.Player1Id} doesn't exists");

                RuleFor(x => x)
                    .MustAsync((o, s, token) => CheckExistingPlayer(o.Player1Id))
                        .WithMessage(player => $"Player2 with id {player.Player2Id} doesn't exists");

                RuleFor(x => x)
                    .MustAsync((o, s, token) => CheckExistingPlayer(o.PlayerAllowedToMoveId))
                        .WithMessage(player => $"Player1 with id {player.PlayerAllowedToMoveId} doesn't exists");

                //TODO Id ходящего игрока должен быть равен id первого или второго игрока
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
