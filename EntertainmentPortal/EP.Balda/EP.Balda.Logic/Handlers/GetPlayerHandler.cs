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
    public class GetPlayerHandler : IRequestHandler<GetPlayer, Maybe<Player>>
    {
        private readonly BaldaGameDbContext _context;
        private readonly IMapper _mapper;

        public GetPlayerHandler(IMapper mapper, BaldaGameDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Maybe<Player>> Handle(GetPlayer request,
                                                CancellationToken cancellationToken)
        {
            var playerDb = await _context.Players
                .Where(p => p.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            return playerDb == null
                ? Maybe<Player>.None
                : Maybe<Player>.From(_mapper.Map<Player>(playerDb));
        }
    }
}