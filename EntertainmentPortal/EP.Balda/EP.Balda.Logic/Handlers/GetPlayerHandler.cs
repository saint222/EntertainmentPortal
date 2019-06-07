using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Balda.Data.Context;
using EP.Balda.Data.Models;
using EP.Balda.Logic.Queries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EP.Balda.Logic.Handlers
{
    public class GetPlayerHandler : IRequestHandler<GetPlayer, Maybe<PlayerDb>>
    {
        private readonly IMapper _mapper;
        private readonly PlayerDbContext _context;

        public GetPlayerHandler(IMapper mapper, PlayerDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Maybe<PlayerDb>> Handle(GetPlayer request, CancellationToken cancellationToken)
        {
            var result = await _context.Players
                .FindAsync(request.Id)
                .ConfigureAwait(false);

            return result == null ?
                Maybe<PlayerDb>.None :
                Maybe<PlayerDb>.From(result);
        }
    }
}
