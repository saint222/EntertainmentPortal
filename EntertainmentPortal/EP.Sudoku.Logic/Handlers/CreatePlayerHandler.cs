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
    public class CreatePlayerHandler : IRequestHandler<CreatePlayer, PlayerDb>
    {
        private readonly IMapper _mapper;
        public CreatePlayerHandler(IMapper mapper)
        {
            _mapper = mapper;
        }
        public Task<PlayerDb> Handle(CreatePlayer request, CancellationToken cancellationToken)
        {
            //var playerDb = PlayerStorage.Players.Add(new PlayerDb((b => _mapper.Map<PlayerDb>(b));
            //*PlayerDb.Where(b => _mapper.Map<PlayerDb>(b));*/
            var playerDb = 
            return Task.FromResult(playerDb);
        }
    }
}
