using EP.SeaBattle.Data.Context;
using EP.SeaBattle.Logic.Commands;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using EP.SeaBattle.Data.Models;
using System.Threading.Tasks;
using System.Linq;

namespace EP.SeaBattle.Logic.Validators
{
    public class AddShotValidation : AbstractValidator<AddShotCommand>
    {
        private readonly SeaBattleDbContext _context;
        public AddShotValidation(SeaBattleDbContext context)
        {
            _context = context;
            RuleSet("AddShotPreValidation", () =>
            {
                RuleFor(ship => ship.X)
                    .InclusiveBetween((byte)0, (byte)9).WithMessage("X must be from 0 to 9");

                RuleFor(ship => ship.Y)
                    .InclusiveBetween((byte)0, (byte)9).WithMessage("Y must be from 0 to 9");

                RuleFor(ship => ship.PlayerId)
                    .NotNull().WithMessage("Player cannot be null");

            });


            RuleSet("AddShotValidation", () =>
            {
                RuleFor(x => x.PlayerId)
                    .MustAsync((o, s, token) => DoesPlayerExist(o.PlayerId)).WithMessage(model => $"Player ID {model.PlayerId} not found");

                RuleFor(x => x.PlayerId)
                    .MustAsync((o, s, token) => DoesPlayerHaveGame(o.PlayerId)).WithMessage(model => $"Player ID {model.PlayerId} does not have a game");

                RuleFor(x => x.PlayerId)
                    .MustAsync((o, s, token) => IsGameFinished(o.PlayerId)).WithMessage(model => $"Game is finished");

                RuleFor(x => x.PlayerId)
                    .MustAsync((o, s, token) => IsReadyToTurn(o.PlayerId)).WithMessage(model => $"Player ID {model.PlayerId} does not have the turn");

                RuleFor(x => x)
                    .MustAsync((o, s, token) => IsThereShot(o.PlayerId, o.X, o.Y)).WithMessage(model => $"Shot with coordinates X {model.X}, Y{model.Y} is forbidden");
            });
        }


        private async Task<bool> DoesPlayerExist(string playerId)
        {
            var result = await _context.Players.FindAsync(playerId)
                .ConfigureAwait(false);
            if (result == null)
            {
                return false;
            }
            return true;
        }

        private async Task<bool> DoesPlayerHaveGame(string playerId)
        {
            PlayerDb playerDb = await _context.Players.FindAsync(playerId)
                .ConfigureAwait(false);
            if (playerDb.GameId == null)
            {
                return false;
            }
            return true;
        }

        private async Task<bool> IsGameFinished(string playerId)
        {
            GameDb gameDb = await _context.Games.FindAsync(playerId).ConfigureAwait(false);
            if (!gameDb.Finish)
            {
                return false;
            }
            return true;
        }

        private async Task<bool> IsReadyToTurn(string playerId)
        {
            GameDb gameDb = await _context.Games.FindAsync(playerId).ConfigureAwait(false);
            if (gameDb.PlayerAllowedToMove != playerId)
            {
                return false;
            }
            return true;
        }

        private async Task<bool> IsThereShot(string playerId, byte x, byte y)
        {
            var result = await _context.Shots.FirstOrDefaultAsync(s => s.PlayerId == playerId && s.X == x && s.Y == y).ConfigureAwait(false);

            if (result != null)
            {
                return false;
            }
            return true;
        }
    }
}
