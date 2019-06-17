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
using System.Linq;
using Microsoft.Extensions.Logging;

namespace EP.SeaBattle.Logic.Handlers
{
    public class DeleteShipHandler : IRequestHandler<DeleteShipCommand, Maybe<IEnumerable<Ship>>>
    {
        private readonly SeaBattleDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteShipCommand> _logger;

        public DeleteShipHandler(SeaBattleDbContext context, IMapper mapper, ILogger<DeleteShipCommand> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Maybe<IEnumerable<Ship>>> Handle(DeleteShipCommand request, CancellationToken cancellationToken)
        {           
            var ship = _context.Ships.Find(request.Id);
            if (ship == null)
            {
                _logger.LogWarning($"Ship with Id {request.Id} not found");
                return Maybe<IEnumerable<Ship>>.From(_mapper.Map<IEnumerable<Ship>>(_context.Ships));
            }
            else
            {
                _context.Ships.Remove(ship);
                try
                {
                    await _context.SaveChangesAsync(cancellationToken);
                    return Maybe<IEnumerable<Ship>>.From(_mapper.Map<IEnumerable<Ship>>(_context.Ships));
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError(ex.Message);
                    return Maybe<IEnumerable<Ship>>.From(_mapper.Map<IEnumerable<Ship>>(_context.Ships));
                }
            }           
        }
    }
}
