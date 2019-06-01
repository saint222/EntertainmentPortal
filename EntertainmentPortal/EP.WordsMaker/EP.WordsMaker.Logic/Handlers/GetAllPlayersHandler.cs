using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using EP.WordsMaker.Data;
using EP.WordsMaker.Logic.Queries;
using EP.WordsMaker.Logic.Models;
using AutoMapper;
using EP.WordsMaker.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EP.WordsMaker.Logic.Handlers
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
                .Select(p => _mapper.Map<Player>(p))
                .ToArrayAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
