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
            //TODO validation for player update
            var player = _context.Players.Find(request.Id);
            if (player == null)
            {
                _logger.LogWarning($"Player with id {request.Id} not found");
                return Result.Fail<Player>("Player not found");
            }

            player.NickName = request.NickName;

            try
            {
                await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                _logger.LogInformation($"Player {player.NickName} changed");
                return Result.Ok<Player>(_mapper.Map<Player>(player));
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message);
                return Result.Fail<Player>("Cannot update player");
            }
        }
    }
}
