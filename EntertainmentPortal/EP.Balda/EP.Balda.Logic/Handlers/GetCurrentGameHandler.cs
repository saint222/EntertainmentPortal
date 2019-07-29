using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Balda.Data.Context;
using EP.Balda.Logic.Models;
using EP.Balda.Logic.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EP.Balda.Logic.Handlers
{
    public class GetCurrentGameHandler : IRequestHandler<GetCurrentGame, Maybe<Game>>
    {
        private readonly BaldaGameDbContext _context;
        private readonly IMapper _mapper;

        public GetCurrentGameHandler(BaldaGameDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Maybe<Game>> Handle(GetCurrentGame request, CancellationToken cancellationToken)
        {
            var playerGame = _context.PlayerGames
                .LastOrDefault(g => g.PlayerId == request.Id);

            if(playerGame == null)
            {
                return Maybe<Game>.None;
            }

            var gameDb = await _context.Games
                .Where(g => g.Id == playerGame.GameId)
                .Include(g => g.Map.Cells)
                .FirstOrDefaultAsync();

            return gameDb?.IsGameOver == true
                ? Maybe<Game>.None
                : Maybe<Game>.From(_mapper.Map<Game>(gameDb));
        }
    }
}
