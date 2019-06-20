using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Balda.Data.Context;
using EP.Balda.Logic.Models;
using EP.Balda.Logic.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EP.Balda.Logic.Handlers
{
    public class GetAllPlayersHandler : IRequestHandler<GetAllPlayers, Maybe<IEnumerable<Player>>>
    {
        private readonly BaldaGameDbContext _context;
        private readonly IMapper _mapper;

        public GetAllPlayersHandler(IMapper mapper, BaldaGameDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Maybe<IEnumerable<Player>>> Handle(
            GetAllPlayers request, CancellationToken cancellationToken)
        {
            var result = await _context.Players
                .ToArrayAsync(cancellationToken)
                .ConfigureAwait(false);
            
            return result.Any()
                ? Maybe<IEnumerable<Player>>.None
                : Maybe<IEnumerable<Player>>.From(_mapper.Map<IEnumerable<Player>>(result));
        }
    }
}