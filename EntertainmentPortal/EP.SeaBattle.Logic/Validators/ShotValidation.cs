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
                .Cascade(CascadeMode.StopOnFirstFailure)
                    .InclusiveBetween((byte)0, (byte)9).WithMessage("X must be from 0 to 9");

                RuleFor(ship => ship.Y)
                .Cascade(CascadeMode.StopOnFirstFailure)
                    .InclusiveBetween((byte)0, (byte)9).WithMessage("Y must be from 0 to 9");

            });


            RuleSet("AddShotValidation", () =>
            {
                RuleFor(x => x.UserId)
                    .MustAsync((o, s, token) => DoesPlayerExist(o.UserId)).WithMessage(model => $"Player ID {model.UserId} not found").DependentRules(() =>
                    {

                        RuleFor(x => x.UserId)
                            .MustAsync((o, s, token) => DoesPlayerHaveGame(o.UserId)).WithMessage(model => $"Player ID {model.UserId} does not have a game");

                        RuleFor(x => x.UserId)
                            .MustAsync((o, s, token) => DoesEnemyPlayerExist(o.UserId)).WithMessage(model => $"Player ID {model.UserId} does not have an enemy player");

                        RuleFor(x => x.UserId)
                            .MustAsync((o, s, token) => IsGameFinished(o.UserId)).WithMessage(model => $"Game is finished");

                        RuleFor(x => x.UserId)
                            .MustAsync((o, s, token) => IsReadyToTurn(o.UserId)).WithMessage(model => $"Player ID {model.UserId} does not have the turn");

                        RuleFor(x => x)
                            .MustAsync((o, s, token) => IsThereShot(o.UserId, o.X, o.Y)).WithMessage(model => $"Shot with coordinates X {model.X}, Y {model.Y} is forbidden");
                    });
            });
        }


        private async Task<bool> DoesPlayerExist(string userId)
        {
            var result = await _context.Players.FirstOrDefaultAsync(p => p.UserId == userId)
                .ConfigureAwait(false);
            if (result == null)
            {
                return false;
            }
            return true;
        }

        private async Task<bool> DoesEnemyPlayerExist(string userId)
        {
            GameDb gameDb = await _context.Games.Where(g => g.Players.Where(p => p.UserId == userId).Count() > 0).FirstOrDefaultAsync().ConfigureAwait(false);
            var result = await _context.Players
                                       .Include(p => p.Ships)
                                       .ThenInclude(s => s.Cells)
                                       .FirstOrDefaultAsync(p => p.GameId == gameDb.Id && p.UserId != userId)
                                       .ConfigureAwait(false);
            if (result == null)
            {
                return false;
            }
            return true;
        }


        private async Task<bool> DoesPlayerHaveGame(string userId)
        {
            PlayerDb playerDb = await _context.Players.FirstOrDefaultAsync(p => p.UserId == userId).ConfigureAwait(false);
            if (playerDb == null || playerDb.GameId == null)
            {
                return false;
            }
            return true;
        }

        private async Task<bool> IsGameFinished(string userId)
        {
            var gameDb = await _context.Games.Where(g => g.Players.Where(p => p.UserId == userId).Count() > 0).FirstOrDefaultAsync().ConfigureAwait(false);
            if (gameDb == null || gameDb.Finish)
            {
                return false; 
            }
            return true;
        }

        private async Task<bool> IsReadyToTurn(string userId)
        {
            GameDb gameDb = await _context.Games.Where(g => g.Players.Where(p => p.UserId == userId).Count() > 0).FirstOrDefaultAsync().ConfigureAwait(false);
            PlayerDb playerDb = await _context.Players.FirstOrDefaultAsync( p=> p.UserId == userId).ConfigureAwait(false);
            if (gameDb == null || gameDb.PlayerAllowedToMove != playerDb.Id)
            {
                return false;
            }
            return true;
        }

        private async Task<bool> IsThereShot(string userId, byte x, byte y)
        {
            PlayerDb playerDb = await _context.Players.FirstOrDefaultAsync(p => p.UserId == userId).ConfigureAwait(false);
            var result = await _context.Shots.FirstOrDefaultAsync(s => s.PlayerId == playerDb.Id && s.X == x && s.Y == y).ConfigureAwait(false);

            if (result != null)
            {
                return false;
            }
            return true;
        }
    }
}
