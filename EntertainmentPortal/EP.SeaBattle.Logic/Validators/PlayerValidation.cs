using EP.SeaBattle.Data.Context;
using EP.SeaBattle.Logic.Commands;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EP.SeaBattle.Logic.Validators
{
    public class PlayerValidation : AbstractValidator<AddNewPlayerCommand>
    {
        private readonly SeaBattleDbContext _context;
        public PlayerValidation(SeaBattleDbContext context)
        {
            _context = context;
            RuleSet("AddPlayerPreValidation", () =>
            {
                RuleFor(x => x.NickName.Trim())
                .NotEmpty().WithMessage("NickName cannot be empty")
                .NotNull().WithMessage("NickName cannot be null")
                .MinimumLength(3).WithMessage("NickName must have 3 characters")
                .MaximumLength(20).WithMessage("NickName must have less 20 characters");
            });

            RuleSet("AddPlayerValidation", () =>
            {
                RuleFor(x => x)
                .MustAsync((o, s, token) => CheckExistingNickName(o.NickName))
                .WithMessage($"Player with such nickname arleady exists");
            });
        }

        private async Task<bool> CheckExistingNickName(string nickName)
        {
            var result = await _context.Players.FirstOrDefaultAsync(player => player.NickName == nickName)
                .ConfigureAwait(false);
            if (result == null)
            {
                return true;
            }
            return false;
        }
    }

    public class PlayerUpdateValidation : AbstractValidator<UpdatePlayerCommand>
    {
        private readonly SeaBattleDbContext _context;
        public PlayerUpdateValidation(SeaBattleDbContext context)
        {
            _context = context;
            RuleSet("UpdatePlayerPreValidation", () =>
            {
                RuleFor(x => x.NickName.Trim())
                .NotEmpty().WithMessage("NickName cannot be empty")
                .MinimumLength(3).WithMessage("NickName must have 3 characters")
                .MaximumLength(20).WithMessage("NickName must have less 20 characters");
            });

            RuleSet("UpdatePlayerValidation", () =>
            {
                RuleFor(x => x)
                .MustAsync((o, s, token) => DoesUserHavePlayer(o.UserId))
                .WithMessage($"User does not have player").DependentRules(() =>
                {
                    RuleSet("UpdatePlayerValidation", () =>
                    {
                        RuleFor(x => x)
                        .MustAsync((o, s, token) => CheckExistingNickName(o.NickName))
                        .WithMessage($"Player doesn not have NickName");
                    });
                });
            });
        }

        private async Task<bool> CheckExistingNickName(string nickName)
        {
            var result = await _context.Players.FirstOrDefaultAsync(player => player.NickName == nickName)
                .ConfigureAwait(false);
            if (result == null)
            {
                return true;
            }
            return false;


        }
        private async Task<bool> DoesUserHavePlayer(string userId)
        {
            var result = await _context.Players.FirstOrDefaultAsync(player => player.UserId == userId)
                .ConfigureAwait(false);
            if (result == null)
            {
                return false;
            }
            return true;
        }
    }
}
