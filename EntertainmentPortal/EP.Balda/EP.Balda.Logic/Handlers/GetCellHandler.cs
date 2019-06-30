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
    public class GetCellHandler : IRequestHandler<GetCell, Maybe<Cell>>
    {
        private readonly BaldaGameDbContext _context;
        private readonly IMapper _mapper;

        public GetCellHandler(BaldaGameDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Maybe<Cell>> Handle(GetCell request,
                                              CancellationToken cancellationToken)
        {
            var cellDb = await _context.Cells
                .Where(c => c.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            return cellDb == null
                ? Maybe<Cell>.None
                : Maybe<Cell>.From(_mapper.Map<Cell>(cellDb));
        }
    }
}