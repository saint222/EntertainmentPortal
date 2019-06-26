using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EP.DotsBoxes.Logic.Queries;
using EP.DotsBoxes.Data.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CSharpFunctionalExtensions;
using EP.DotsBoxes.Data.Models;

namespace EP.DotsBoxes.Logic.Handlers
{
    public class GetAllPlayersHandler : IRequestHandler<GetAllPlayers, Maybe<IEnumerable<PlayerDb>>>
    {
        private readonly IMapper _mapper;
        private readonly GameBoardDbContext _context;

        public GetAllPlayersHandler(IMapper mapper, GameBoardDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Maybe<IEnumerable<PlayerDb>>> Handle(GetAllPlayers request, CancellationToken cancellationToken)
        {
            var result = await _context.Players
               .AsNoTracking()
               .ToArrayAsync(cancellationToken)
               .ConfigureAwait(false);

            return result.Any() ? 
                Maybe<IEnumerable<PlayerDb>>.From(result) : 
                Maybe<IEnumerable<PlayerDb>>.None;
        }
    }
}
