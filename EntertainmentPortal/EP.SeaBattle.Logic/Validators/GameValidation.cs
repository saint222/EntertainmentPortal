using EP.SeaBattle.Data.Context;
using EP.SeaBattle.Logic.Commands;
using EP.SeaBattle.Logic.Models;
using EP.SeaBattle.Data.Models;
using FluentValidation;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace EP.SeaBattle.Logic.Validators
{
    //TODO объявить названия валидаций как константы и использовать константы
    public class GameValidation : AbstractValidator<StartGameCommand>
    {
        private SeaBattleDbContext _context;
        private readonly IMapper _mapper;
        public GameValidation(SeaBattleDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

            RuleSet("GameValidation", () =>
            {
                RuleFor(x => x)
                        .MustAsync((x, s, token) => CheckExistingPlayer(x.UserId))
                        .WithMessage(player => $"Player doesn't exists!!!").DependentRules(()=> {
                            RuleFor(command => command)
                                      .MustAsync((o, s, token) => IsFullShipsSet(o.UserId))
                                      .WithMessage(player => $"Player does not have enough ships.");

                            RuleFor(command => command)
                                    .MustAsync((o, s, token) => IsNotInGame(o.UserId))
                                    .WithMessage(player => $"Player already in the game.");
                        });


            });

        }

        private async Task<bool> IsNotInGame(string userId)
        {
            PlayerDb playerDb = await _context.Players.FirstOrDefaultAsync( p => p.UserId == userId).ConfigureAwait(false);
            if (playerDb.GameId != null)
            {
                return false;
            }
            return true;
        }

        private async Task<bool> CheckExistingPlayer(string userId)
        {
            var result = await _context.Players.FirstOrDefaultAsync(p => p.UserId == userId).ConfigureAwait(false);
            if (result == null)
                return false;
            return true;
        }

        private async Task<bool> IsFullShipsSet(string userId)
        {

            PlayerDb playerDb = await _context.Players
                                                .Include(p => p.Ships)
                                                .FirstOrDefaultAsync(p => p.UserId == userId)
                                                .ConfigureAwait(false);
            ShipsManager shipsManager = new ShipsManager(_mapper.Map<Player>(playerDb));

            return shipsManager.IsFull;
        }
    }
}

