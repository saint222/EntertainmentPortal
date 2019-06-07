using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Balda.Data.Context;
using EP.Balda.Data.Models;
using EP.Balda.Logic.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EP.Balda.Logic.Handlers
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
                .ToArrayAsync(cancellationToken)
                .ConfigureAwait(false);

            return result.Any() ?
                Maybe<IEnumerable<PlayerDb>>.None :
                Maybe<IEnumerable<PlayerDb>>.From(result);
        }
    }
}