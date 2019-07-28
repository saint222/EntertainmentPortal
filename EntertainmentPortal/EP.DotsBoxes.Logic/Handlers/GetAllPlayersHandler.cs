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
using EP.DotsBoxes.Logic.Models;

namespace EP.DotsBoxes.Logic.Handlers
{
    public class GetAllPlayersHandler : IRequestHandler<GetAllPlayers, Maybe<IEnumerable<Player>>>
    {
        private readonly IMapper _mapper;
        private readonly GameBoardDbContext _context;       

        public GetAllPlayersHandler(IMapper mapper, GameBoardDbContext context)
        {
            _mapper = mapper;
            _context = context;           
        }

        public async Task<Maybe<IEnumerable<Player>>> Handle(GetAllPlayers request, CancellationToken cancellationToken)
        {            
            var player = await _context.Players
               .AsNoTracking()
               .ToListAsync(cancellationToken);

            var result = _mapper.Map<List<PlayerDb>, IEnumerable<Player>>(player);            
            
            return result.Any() ?
                Maybe<IEnumerable<Player>>.From(result) :
                Maybe<IEnumerable<Player>>.None;
        }
    }
}
