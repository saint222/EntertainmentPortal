using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EP.DotsBoxes.Logic.Queries;
using EP.DotsBoxes.Data;
using EP.DotsBoxes.Data.Context;
using EP.DotsBoxes.Logic.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EP.DotsBoxes.Logic.Handlers
{
    public class GetAllPlayersHandler : IRequestHandler<GetAllPlayers, IEnumerable<Player>>
    {
        private readonly IMapper _mapper;
        private readonly PlayerDbContext _context;

        public GetAllPlayersHandler(IMapper mapper, PlayerDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<Player>> Handle(GetAllPlayers request, CancellationToken cancellationToken)
        {
            return await _context.Players
                .Select(b => _mapper.Map<Player>(b))
                .ToArrayAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
