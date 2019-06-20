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
    public class GetMapHandler : IRequestHandler<GetMap, Maybe<Map>>
    {
        private readonly IMapper _mapper;
        private readonly BaldaGameDbContext _context;

        public GetMapHandler(IMapper mapper, BaldaGameDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Maybe<Map>> Handle(GetMap request, CancellationToken cancellationToken)
        {
            var result = await _context.Maps
                .FindAsync(request.Id)
                .ConfigureAwait(false);

            return result == null ? Maybe<Map>.None : Maybe<Map>.From(_mapper.Map<Map>(result));
        }
    }
}
