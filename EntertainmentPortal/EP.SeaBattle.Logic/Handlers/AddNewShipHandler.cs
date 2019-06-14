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
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace EP.SeaBattle.Logic.Handlers
{
    public class AddNewShipHandler : IRequestHandler<AddNewShipCommand, Maybe<IEnumerable<Ship>>>
    {
        private readonly SeaBattleDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<AddNewShipCommand> _validator;
        private readonly ILogger _logger;

        public AddNewShipHandler(SeaBattleDbContext context, IMapper mapper, IValidator<AddNewShipCommand> validator, ILogger<AddNewShipHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Maybe<IEnumerable<Ship>>> Handle(AddNewShipCommand request, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(request, ruleSet: "PL Ship Add Validation", cancellationToken: cancellationToken)
                .ConfigureAwait(false);
            if (result.IsValid)
            {
                ShipsManager shipsManager = new ShipsManager(request.Game, request.Player, new List<Ship>());
                var wasAdded = shipsManager.AddShip(request.X, request.Y, request.Orientation, request.Rank);
                if (wasAdded)
                {
                    _context.Ships.AddRange(_mapper.Map<IEnumerable<ShipDb>>( shipsManager.Ships));
                    try
                    {
                        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                        return Maybe<IEnumerable<Ship>>.From(_mapper.Map<IEnumerable<Ship>>(_context.Ships));
                    }
                    catch (DbUpdateException ex)
                    {
                        _logger.LogError(ex.Message);
                        return Maybe<IEnumerable<Ship>>.None;
                    }
                }
                else
                {
                    _logger.LogInformation($"Ship was not added to the field. " +
                        $"Ship info X: {request.X} Y: {request.Y}, Orientation {request.Orientation}, Rank {request.Rank}, " +
                        $"Game: {request.GameId}, Player {request.PlayerId}");
                    return Maybe<IEnumerable<Ship>>.From(_mapper.Map<IEnumerable<Ship>>(_context.Ships));
                }
                
            }
            else
            {
                _logger.LogInformation(string.Join(", ", result.Errors));
                return Maybe<IEnumerable<Ship>>.From(_mapper.Map<IEnumerable<Ship>>(_context.Ships));
            }
            
        }
    }
}
