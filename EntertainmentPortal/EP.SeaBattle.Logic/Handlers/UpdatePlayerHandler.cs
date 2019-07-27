using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EP.SeaBattle.Data.Context;
using EP.SeaBattle.Data.Models;
using EP.SeaBattle.Logic.Commands;
using EP.SeaBattle.Logic.Models;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EP.SeaBattle.Logic.Handlers
{
    public class UpdatePlayerHandler : IRequestHandler<UpdatePlayerCommand, Result<Player>>
    {
        private readonly SeaBattleDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdatePlayerCommand> _validator;
        private readonly ILogger _logger;

        public UpdatePlayerHandler(SeaBattleDbContext context, IMapper mapper, IValidator<UpdatePlayerCommand> validator, ILogger<UpdatePlayerHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<Player>> Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
        {
            PlayerDb playerdb = _context.Players.Find(request.UserId);
            if (playerdb == null)
            {
                _logger.LogWarning($"Player not found");
                return Result.Fail<Player>("Player not found");
            }

            var validationResult = await _validator.ValidateAsync(request, ruleSet: "UpdatePlayerValidation", cancellationToken: cancellationToken);
            if (validationResult.IsValid)
            {
                playerdb.NickName = request.NickName;

                try
                {
                    await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                    _logger.LogInformation($"The Player changed the NickName");
                    return Result.Ok<Player>(_mapper.Map<Player>(playerdb));
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError(ex.Message);
                    return Result.Fail<Player>("Cannot update the player in the data base");
                }
            }
            else
            {
                _logger.LogInformation($"The player is not valid");
                return Result.Fail<Player>(validationResult.Errors.First().ErrorMessage);
            }
            
        }
    }
}
