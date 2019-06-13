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
    public class AddNewShipHandler : IRequestHandler<AddNewShipCommand, Maybe<IEnumerable<Ship>>>
    {
        private readonly SeaBattleDbContext _context;
        private readonly IMapper _mapper;


        public AddNewShipHandler(SeaBattleDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Maybe<IEnumerable<Ship>>> Handle(AddNewShipCommand request, CancellationToken cancellationToken)
        {
            if (true)
            {
                ShipsManager shipsManager = new ShipsManager(request.Game, request.Player, new List<Ship>());
                var result = shipsManager.AddShip(request.X, request.Y, request.Orientation, request.ShipRank);
                if (result)
                {
                    _context.Ships.AddRange(_mapper.Map<IEnumerable<ShipDb>>( shipsManager.Ships));
                }
                
                try
                {
                    await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                    return Maybe<IEnumerable<Ship>>.From(_mapper.Map<IEnumerable<Ship>>(_context.Ships));
                }
                catch (DbUpdateException ex)
                {

                    return Maybe<IEnumerable<Ship>>.None;
                }
            }
            //else
                //return Result.Fail<Ship>(string.Join("; ", result.Errors));
        }
    }
}
