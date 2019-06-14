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

namespace EP.SeaBattle.Logic.Handlers
{
    public class DeleteShipHandler : IRequestHandler<DeleteShipCommand, Maybe<IEnumerable<Ship>>>
    {
        private readonly SeaBattleDbContext _context;
        private readonly IMapper _mapper;

        public DeleteShipHandler(SeaBattleDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Maybe<IEnumerable<Ship>>> Handle(DeleteShipCommand request, CancellationToken cancellationToken)
        {
            //TODO Validation for deleting ship
            if (true)
            {
                var ship = _context.Ships.Find(request.Id);

                _context.Ships.Remove(ship);
                await _context.SaveChangesAsync(cancellationToken);
                return Maybe<IEnumerable<Ship>>.From(_mapper.Map<IEnumerable<Ship>>(_context.Ships));
                //TODO Logging
            }
        }
    }
}
