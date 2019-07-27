using EP.SeaBattle.Data.Context;
using EP.SeaBattle.Logic.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EP.SeaBattle.Logic.Validators
{
    //TODO объявить названия валидаций как константы и использовать константы
    public class ShipAddValidation : AbstractValidator<AddNewShipCommand>
    {
        SeaBattleDbContext _context;
        public ShipAddValidation(SeaBattleDbContext context)
        {
            _context = context;
            RuleSet("AddShipPreValidation", () =>
            {
                RuleFor(ship => ship.X)
                    .InclusiveBetween((byte)0, (byte)9).WithMessage("X must be from 0 to 9");

                RuleFor(ship => ship.Y)
                    .InclusiveBetween((byte)0, (byte)9).WithMessage("Y must be from 0 to 9");

                RuleFor(ship => ship.Orientation)
                    .IsInEnum().WithMessage("Orientation incorrect");

                RuleFor(ship => ship.Rank)
                    .IsInEnum().WithMessage("Rank incorrect");

                RuleFor(ship => ship.PlayerId)
                    .NotNull().WithMessage("Player cannot be null");

                RuleFor(ship => ship.GameId)
                    .NotNull().WithMessage("Game cannot be null");
            });
            RuleSet("AddShipValidation", () =>
            {
                RuleFor(x => x.PlayerId)
                    .MustAsync((o, s, token) => CheckExistingPlayer(o)).WithMessage(model => $"Player ID {model.PlayerId} not found");
                RuleFor(x => x.GameId)
                    .MustAsync((o, s, token) => CheckExistingGameAndGameNotFinish(o)).WithMessage(model => $"Game ID {model.GameId} not found or finished");
            });
        }

        private async Task<bool> CheckExistingPlayer(AddNewShipCommand model)
        {
            var result = await _context.Players.FindAsync(model.PlayerId)
                .ConfigureAwait(false);
            if (result == null)
                return false;
            return true;
        }

        private async Task<bool> CheckExistingGameAndGameNotFinish(AddNewShipCommand model)
        {
            var result = await _context.Games.FindAsync(model.GameId)
                .ConfigureAwait(false);
            if (result == null || result.Status == Common.Enums.GameStatus.Finished)
                return false;
            return true;
        }
    }

    public class ShipDeleteValidation : AbstractValidator<DeleteShipCommand>
    {
        SeaBattleDbContext _context;
        public ShipDeleteValidation(SeaBattleDbContext context)
        {
            _context = context;

            RuleSet("DeleteShipPreValidation", () =>
            {
                RuleFor(ship => ship.X)
                    .ExclusiveBetween((byte)0, (byte)9).WithMessage("X must be from 0 to 9");
                RuleFor(ship => ship.Y)
                    .ExclusiveBetween((byte)0, (byte)9).WithMessage("Y must be from 0 to 9");
            });

            RuleSet("DeleteShipValidation", () =>
            {
                //RuleFor(x => x)
                //    .MustAsync(
                //        (o, s, token) => CheckExistingShip(o)
                //           ).WithMessage("Ship not exists");
            });
        }

        //private async Task<bool> CheckExistingShip(DeleteShipCommand model)
        //{
        //    var result = await _context.Ships.FindAsync(model.Id)
        //        .ConfigureAwait(false);

        //    if (result == null)
        //        return false;
        //    return true;
        //}
    }
}
