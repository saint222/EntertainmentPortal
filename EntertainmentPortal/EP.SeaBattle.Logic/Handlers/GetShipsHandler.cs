using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EP.SeaBattle.Data.Context;
using EP.SeaBattle.Data.Models;
using EP.SeaBattle.Logic.Queries;
using EP.SeaBattle.Logic.Models;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EP.SeaBattle.Logic.Handlers
{
    public class GetShipsHandler : IRequestHandler<GetShipsQuery, Maybe<IEnumerable<Ship>>>
    {
        private readonly SeaBattleDbContext _context;
        private readonly IMapper _mapper;


        public GetShipsHandler(SeaBattleDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Maybe<IEnumerable<Ship>>> Handle(GetShipsQuery request, CancellationToken cancellationToken)
        {
            //TODO Validation for geting all ship
            if (true)
            {

                var result = await _context.Ships.Where(w => w.Game.Id == request.GameId && w.Player.Id == request.PlayerId)
                    .Include(i => i.Cells)
                    .Include(i => i.Game)
                    .Include(i => i.Player)
                    .ToArrayAsync(cancellationToken).ConfigureAwait(false);

                return result.Any() ? Maybe<IEnumerable<Ship>>.From(_mapper.Map<IEnumerable<Ship>>(result))
                    : Maybe<IEnumerable<Ship>>.None;
                //TODO Logging
            }
        }
    }
}
