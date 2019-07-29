using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Balda.Data.Context;
using EP.Balda.Logic.Models;
using EP.Balda.Logic.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EP.Balda.Logic.Handlers
{
    public class GetGameResultsHandler : IRequestHandler<GetGameResults, Maybe<Game>>
    {
        private readonly BaldaGameDbContext _context;
        private readonly IMapper _mapper;

        public GetGameResultsHandler(BaldaGameDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Maybe<Game>> Handle(GetGameResults request, CancellationToken cancellationToken)
        {
            var playerGame = await _context.PlayerGames
                .Where(g => g.PlayerId == request.PlayerId)
                .LastOrDefaultAsync();

            if(playerGame == null)
            {
                return Maybe<Game>.None;
            }

            var gameDb = await _context.Games
                .Where(g => g.Id == playerGame.GameId && g.IsGameOver == true)
                .FirstOrDefaultAsync(cancellationToken);

            return gameDb == null
                ? Maybe<Game>.None
                : Maybe<Game>.From(_mapper.Map<Game>(gameDb));
        }
    }
}