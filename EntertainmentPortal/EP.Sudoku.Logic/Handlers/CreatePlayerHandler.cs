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
    public class CreatePlayerHandler : IRequestHandler<CreatePlayer, Player>
    {
        private readonly IMapper _mapper;
        public CreatePlayerHandler(IMapper mapper)
        {
            _mapper = mapper;
        }
        public Task<Player> Handle(CreatePlayer request, CancellationToken cancellationToken)
        {
            var playerDb = _mapper.Map<PlayerDb>(request.player);
            PlayerStorage.Players.Add(playerDb);
            return Task.FromResult(request.player);
        }
    }
}
