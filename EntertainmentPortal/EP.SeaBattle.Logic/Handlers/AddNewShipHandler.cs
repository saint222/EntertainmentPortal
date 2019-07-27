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
using System.Linq;

namespace EP.SeaBattle.Logic.Handlers
{
    public class AddNewShipHandler : IRequestHandler<AddNewShipCommand, Result<IEnumerable<Ship>>>
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

        public async Task<Result<IEnumerable<Ship>>> Handle(AddNewShipCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, ruleSet: "AddShipValidation", cancellationToken: cancellationToken);
            if (validationResult.IsValid)
            {
                PlayerDb playerDb = await _context.Players
                                                    .Include(p => p.Ships)
                                                    .ThenInclude(s => s.Cells)
                                                    .FirstOrDefaultAsync(p => p.UserId == request.UserId)
                                                    .ConfigureAwait(false);

                Player player = _mapper.Map<Player>(playerDb);
                ShipsManager shipsManager = new ShipsManager(player);


                if (shipsManager.AddShip(request.X, request.Y, request.Orientation, request.Rank, out Ship ship))
                {
                    ShipDb shipDb = _mapper.Map<ShipDb>(ship);
                    shipDb.PlayerId = playerDb.Id;
                    playerDb.Ships.Add(shipDb);
                    try
                    {
                        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                        return Result.Ok(_mapper.Map<IEnumerable<Ship>>(playerDb.Ships));
                    }
                    catch (DbUpdateException ex)
                    {
                        _logger.LogError(ex.Message);
                        return Result.Fail<IEnumerable<Ship>>("Cannot add ship");
                    }
                }
                else
                {
                    _logger.LogInformation($"Ship was not added to the field. " +
                        $"Ship info X: {request.X} Y: {request.Y}, Orientation {request.Orientation}, Rank {request.Rank}, " +
                        $" User {request.UserId}");
                    return Result.Fail<IEnumerable<Ship>>("Ship was not added to the field");
                }
            }
            else
            {
                _logger.LogInformation(string.Join(", ", validationResult.Errors));
                return Result.Fail<IEnumerable<Ship>>(validationResult.Errors.First().ErrorMessage);
            }
        }
    }
}
