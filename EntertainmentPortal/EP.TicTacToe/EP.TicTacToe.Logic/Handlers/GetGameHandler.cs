using AutoMapper;
using CSharpFunctionalExtensions;
using EP.TicTacToe.Data.Context;
using EP.TicTacToe.Logic.Models;
using EP.TicTacToe.Logic.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EP.TicTacToe.Logic.Handlers
{
    public class GetGameHandler : IRequestHandler<GetGame, Maybe<Game>>
    {
        private readonly TicTacDbContext _context;
        private readonly IMapper _mapper;

        public GetGameHandler(TicTacDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Maybe<Game>> Handle(GetGame request, CancellationToken cancellationToken)
        {
            var gameDb = await _context.Games
                .Where(g => g.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            return gameDb == null
                ? Maybe<Game>.None
                : Maybe<Game>.From(_mapper.Map<Game>(gameDb));
        }
    }
}