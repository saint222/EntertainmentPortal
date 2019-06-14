using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.SeaBattle.Data.Context;
using EP.SeaBattle.Data.Models;
using EP.SeaBattle.Logic.Commands;
using EP.SeaBattle.Logic.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EP.SeaBattle.Logic.Handlers
{
    public class AddNewPlayerHandler : IRequestHandler<AddNewPlayerCommand, Result<Player>>
    {
        private readonly SeaBattleDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<AddNewPlayerCommand> _validator;
        private readonly ILogger _logger;

        public AddNewPlayerHandler(SeaBattleDbContext context, IMapper mapper, IValidator<AddNewPlayerCommand> validator, ILogger<AddNewPlayerHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<Player>> Handle(AddNewPlayerCommand request, CancellationToken cancellationToken)
        {
            _context.Players.Add(_mapper.Map<PlayerDb>(request));
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Player with NickName {request.NickName} saved");
                return Result.Ok(_mapper.Map<Player>(request));
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message);
                return Result.Fail<Player>("Cannot add player");
            }
        }
    }
}
