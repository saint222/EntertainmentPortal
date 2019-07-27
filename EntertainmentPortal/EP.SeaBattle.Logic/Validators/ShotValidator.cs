using EP.SeaBattle.Data.Context;
using EP.SeaBattle.Logic.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EP.SeaBattle.Logic.Validators
{
    public class ShotValidator : AbstractValidator<AddShotCommand>
    {
        SeaBattleDbContext _context;
        public ShotValidator(SeaBattleDbContext context)
        {
            _context = context;
            RuleSet("AddShotPreValidation", () =>
            {
                RuleFor(shot => shot.X)
                    .InclusiveBetween((byte)0, (byte)9).WithMessage("X must be from 0 to 9");

                RuleFor(shot => shot.Y)
                    .InclusiveBetween((byte)0, (byte)9).WithMessage("Y must be from 0 to 9");
            });

            RuleSet("AddShotValidation", () =>
            {
                RuleFor(x => x.PlayerId)
                    .MustAsync((o, s, token) => CheckExistingPlayer(o)).WithMessage(model => $"Player ID {model.PlayerId} not found");
                RuleFor(x => x.GameId)
                    .MustAsync((o, s, token) => CheckExistingGameAndGameNotFinish(o)).WithMessage(model => $"Game ID {model.GameId} not found or finished");
            });
        }

        private async Task<bool> CheckExistingPlayer(AddShotCommand model)
        {
            var result = await _context.Players.FindAsync(model.PlayerId)
                .ConfigureAwait(false);
            if (result == null)
                return false;
            return true;
        }

        private async Task<bool> CheckExistingGameAndGameNotFinish(AddShotCommand model)
        {
            var result = await _context.Games.FindAsync(model.GameId)
                .ConfigureAwait(false);
            if (result == null || result.Status == Common.Enums.GameStatus.Finished)
                return false;
            return true;
        }
    }
}
