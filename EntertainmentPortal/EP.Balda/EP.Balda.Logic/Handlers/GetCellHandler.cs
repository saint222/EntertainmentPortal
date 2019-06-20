using System.Threading;
using System.Threading.Tasks;
using EP.Balda.Logic.Models;
using EP.Balda.Logic.Queries;
using MediatR;
using CSharpFunctionalExtensions;
using AutoMapper;
using EP.Balda.Data.Context;

namespace EP.Balda.Logic.Handlers
{
    public class GetCellHandler : IRequestHandler<GetCell, Maybe<Cell>>
    {
        private readonly IMapper _mapper;
        private readonly BaldaGameDbContext _context;

        public GetCellHandler(IMapper mapper, BaldaGameDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Maybe<Cell>> Handle(GetCell request, CancellationToken cancellationToken)
        {
            var result = await _context.Cells
                .FindAsync(request.Id)
                .ConfigureAwait(false);

            return result == null ? Maybe<Cell>.None : Maybe<Cell>.From(_mapper.Map<Cell>(result));
        }
    }
}
