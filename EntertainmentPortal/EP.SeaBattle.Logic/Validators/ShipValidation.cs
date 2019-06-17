using EP.SeaBattle.Data.Context;
using EP.SeaBattle.Logic.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EP.SeaBattle.Logic.Validators
{
    public class ShipAddValidation : AbstractValidator<AddNewShipCommand>
    {
        //TODO проверить валидацию (не работает на уровне представления)
        public ShipAddValidation(SeaBattleDbContext context)
        {

            RuleFor(ship => ship.X)
                .InclusiveBetween((byte)0, (byte)9).WithMessage("X must be from 0 to 9");

            RuleFor(ship => ship.Y)
                .InclusiveBetween((byte)0, (byte)9).WithMessage("Y must be from 0 to 9");

            RuleFor(ship => ship.Orientation)
                .IsInEnum().WithMessage("Orientation incorrect");

            RuleFor(ship => ship.Rank)
                .IsInEnum().WithMessage("Rank incorrect");

            RuleFor(ship => ship.Player)
                .NotNull().WithMessage("Player cannot be null");

            RuleFor(ship => ship.Game)
                .NotNull().WithMessage("Game cannot be null");
        }
    }

    public class ShipDeleteValidation : AbstractValidator<DeleteShipCommand>
    {
        SeaBattleDbContext _context;
        public ShipDeleteValidation(SeaBattleDbContext context)
        {
            _context = context;

            RuleSet("PL Ship Delete Validation", () =>
            {
                RuleFor(ship => ship.Id.Trim())
                    .NotEmpty().WithMessage("Id cannot be null");
            });

            RuleSet("BL Ship Delete Validation", () =>
            {
                RuleFor(x => x)
                    .MustAsync(
                        (o, s, token) => CheckExistingShip(o)
                           ).WithMessage("Ship not exists");
            });
        }

        private async Task<bool> CheckExistingShip(DeleteShipCommand model)
        {
            var result = await _context.Ships.FindAsync(model.Id)
                .ConfigureAwait(false);

            if (result == null)
                return false;
            return true;
        }
    }
}
