using System.Threading;
using System.Threading.Tasks;
using EP.Balda.Logic.Models;
using EP.Balda.Logic.Queries;
using MediatR;
using CSharpFunctionalExtensions;
using AutoMapper;
using EP.Balda.Data.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using EP.Balda.Data.Models;

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
            var cellDb = await (_context.Cells
                .Where(c => c.Id == request.Id)
                .FirstOrDefaultAsync<CellDb>());

            return cellDb == null ? 
                Maybe<Cell>.None : 
                Maybe<Cell>.From(_mapper.Map<Cell>(cellDb));
        }
    }
}
