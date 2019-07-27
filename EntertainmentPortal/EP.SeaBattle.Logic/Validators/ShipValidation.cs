using EP.SeaBattle.Data.Context;
using EP.SeaBattle.Logic.Commands;
using FluentValidation;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EP.SeaBattle.Logic.Validators
{
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
            });
            RuleSet("AddShipValidation", () =>
            {
                RuleFor(x => x.UserId)
                    .MustAsync((o, s, token) => CheckExistingPlayer(o)).WithMessage(model => $"Player not found");
            });
        }



        private async Task<bool> CheckExistingPlayer(AddNewShipCommand model)
        {
            var result = await _context.Players.FirstOrDefaultAsync(p => p.UserId == model.UserId).ConfigureAwait(false);
            if (result == null)
            {
                return false;
            }
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
                RuleFor(ship => ship.Id.Trim())
                    .NotEmpty().WithMessage("Id cannot be null");
            });

            RuleSet("DeleteShipValidation", () =>
            {
                RuleFor(x => x)
                    .MustAsync(
                        (o, s, token) => CheckExistingShip(o.Id)
                           ).WithMessage("The ship does not exist");
            });
        }

        private async Task<bool> CheckExistingShip(string id)
        {
            var result = await _context.Ships.FindAsync(id).ConfigureAwait(false);
            if (result == null)
            {
                return false;
            }
            return true;
        }
    }
}
