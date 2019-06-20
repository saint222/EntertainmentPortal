using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Balda.Data.Context;
using EP.Balda.Logic.Models;
using EP.Balda.Logic.Queries;
using MediatR;

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
            var result = await _context.Players
                .FindAsync(request.Id)
                .ConfigureAwait(false);

            return result == null ? Maybe<Player>.None : Maybe<Player>.From(_mapper.Map<Player>(result));
        }
    }
}