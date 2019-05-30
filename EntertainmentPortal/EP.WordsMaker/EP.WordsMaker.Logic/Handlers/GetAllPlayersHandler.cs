using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using EP.WordsMaker.Data;
using EP.WordsMaker.Logic.Queries;
using EP.WordsMaker.Logic.Models;
using AutoMapper;

namespace EP.WordsMaker.Logic.Handlers
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
            var items = PlayerStorage.Players.Select(p => _mapper.Map<Player>(p)).ToArray();
            return Task.FromResult((IEnumerable<Player>) items);                                                                                                             
        }
    }
}
