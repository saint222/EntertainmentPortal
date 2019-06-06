using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EP.Hangman.Data.Models;
using MediatR;
using EP.Hangman.Logic.Models;
using EP.Hangman.Logic.Queries;
using EP.Hangman.Data.Context;

namespace EP.Hangman.Logic.Handlers
{
    public class GetUserSessionHandler : IRequestHandler<GetUserSession, ControllerData>
    {
        private readonly GameDbContext _context;

        private readonly IMapper _mapper;

        public GetUserSessionHandler(GameDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ControllerData> Handle(GetUserSession request, CancellationToken cancellationToken)
        {
            return _mapper.Map<UserGameData, ControllerData>(_mapper.Map<GameDb, UserGameData>(await _context.Games.FindAsync(request._data.Id)));
        }
    }
}
