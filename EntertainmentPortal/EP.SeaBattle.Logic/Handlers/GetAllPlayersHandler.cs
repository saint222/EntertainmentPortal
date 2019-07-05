using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EP.SeaBattle.Data.Context;
using EP.SeaBattle.Logic.Queries;
using EP.SeaBattle.Logic.Models;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EP.SeaBattle.Logic.Handlers
{
    public class GetAllPlayersHandler : IRequestHandler<GetAllPlayersQuery, Maybe<IEnumerable<Player>>>
    {
        private readonly SeaBattleDbContext _context;
        private readonly IMapper _mapper;

        public GetAllPlayersHandler(SeaBattleDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Maybe<IEnumerable<Player>>> Handle(GetAllPlayersQuery request, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<IEnumerable<Player>>(await _context.Players
                                                                    .Include(p => p.Ships)
                                                                    .AsNoTracking()
                                                                    .ToArrayAsync(cancellationToken)
                                                                    .ConfigureAwait(false));

            return !result.Any() ?
                Maybe<IEnumerable<Player>>.None :
                Maybe<IEnumerable<Player>>.From(result);
        }
    }
}
