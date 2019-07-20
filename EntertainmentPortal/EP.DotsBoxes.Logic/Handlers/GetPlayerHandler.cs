using AutoMapper;
using CSharpFunctionalExtensions;
using EP.DotsBoxes.Data.Context;
using EP.DotsBoxes.Logic.Models;
using EP.DotsBoxes.Logic.Queries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EP.DotsBoxes.Logic.Handlers
{
    public class GetPlayerHandler : IRequestHandler<GetPlayer, Maybe<Player>>
    {
        private readonly IMapper _mapper;
        private readonly GameBoardDbContext _context;

        public GetPlayerHandler(IMapper mapper, GameBoardDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Maybe<Player>> Handle(GetPlayer request, CancellationToken cancellationToken)
        {
            var result = await _context.Players
                .FindAsync(request.Id);

            return result == null ? Maybe<Player>.None : Maybe<Player>.From(_mapper.Map<Player>(result));
        }
    }
}
