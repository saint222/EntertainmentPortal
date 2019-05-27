using AutoMapper;
using EP.Sudoku.Data;
using EP.Sudoku.Data.Models;
using EP.Sudoku.Logic.Models;
using EP.Sudoku.Logic.Queries;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EP.Sudoku.Logic.Handlers
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
            var items = PlayerStorage.Players.Select(b => _mapper.Map<Player>(b)).ToArray();            
            return Task.FromResult((IEnumerable<Player>)items);
        }
    }
}
