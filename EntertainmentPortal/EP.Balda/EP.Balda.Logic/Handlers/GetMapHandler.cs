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
    public class GetMapHandler : IRequestHandler<GetMap, Maybe<Map>>
    {
        private readonly BaldaGameDbContext _context;
        private readonly IMapper _mapper;

        public GetMapHandler(IMapper mapper, BaldaGameDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Maybe<Map>> Handle(GetMap request,
                                             CancellationToken cancellationToken)
        {
            var mapDb = await _context.Maps
                .Where(m => m.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            return mapDb == null
                ? Maybe<Map>.None
                : Maybe<Map>.From(_mapper.Map<Map>(mapDb));
        }
    }
}