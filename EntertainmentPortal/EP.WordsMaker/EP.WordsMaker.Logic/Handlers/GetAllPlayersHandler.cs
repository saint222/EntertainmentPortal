using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using EP.WordsMaker.Data;
using EP.WordsMaker.Logic.Queries;
using EP.WordsMaker.Logic.Models;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.WordsMaker.Data.Context;
using EP.WordsMaker.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace EP.WordsMaker.Logic.Handlers
{
    public class GetAllPlayersHandler : IRequestHandler<GetAllPlayers, Maybe<IEnumerable<PlayerDb>>>
    {
        private readonly IMapper _mapper;
        private readonly PlayerDbContext _context;

        public GetAllPlayersHandler(IMapper mapper, PlayerDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Maybe<IEnumerable<PlayerDb>>> Handle(GetAllPlayers request, CancellationToken cancellationToken)
        {
            var result = await _context.Players
                .Include(x => x.Name)
                .AsNoTracking()
                .ToArrayAsync(cancellationToken)
                .ConfigureAwait(false);

            return  result.Any() ?
                Maybe<IEnumerable<PlayerDb>>.None :
                Maybe<IEnumerable<PlayerDb>>.From(result);
        }
    }
}
