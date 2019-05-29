using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EP.DotsBoxes.Logic.Queries;
using EP.DotsBoxes.Data;
using EP.DotsBoxes.Logic.Models;
using MediatR;

namespace EP.DotsBoxes.Logic.Handlers
{
    public class GetAllPlayersHandler : IRequestHandler<GetAllPlayers, IEnumerable<Player>>
    {
        private readonly IMapper _mapper;

        public GetAllPlayersHandler(IMapper mapper)
        {
            _mapper = mapper;
        }
        public Task<IEnumerable<Player>> Handle(GetAllPlayers request, CancellationToken cancellationToken)
        {
            var items = PlayerStatistics.Players.Select(b => _mapper.Map<Player>(b)).ToArray();
          
            return Task.FromResult((IEnumerable<Player>)items);
        }
    }
}
