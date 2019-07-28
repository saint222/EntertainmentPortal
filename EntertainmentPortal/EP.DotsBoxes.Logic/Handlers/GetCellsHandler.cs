using AutoMapper;
using CSharpFunctionalExtensions;
using EP.DotsBoxes.Data.Context;
using EP.DotsBoxes.Data.Models;
using EP.DotsBoxes.Logic.Models;
using EP.DotsBoxes.Logic.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EP.DotsBoxes.Logic.Handlers
{
    public class GetCellsHandler : IRequestHandler<GetCells, Maybe<IEnumerable<Cell>>>
    {
        private readonly IMapper _mapper;
        private readonly GameBoardDbContext _context;
        
        public GetCellsHandler(IMapper mapper, GameBoardDbContext context)
        {
            _mapper = mapper;
            _context = context;            
        }
        
        public async Task<Maybe<IEnumerable<Cell>>> Handle(GetCells request, CancellationToken cancellationToken)
        {     
            var cells = await _context.Cells
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var result = _mapper.Map<List<CellDb>, IEnumerable<Cell>>(cells);            

            return result.Any() ?
                Maybe<IEnumerable<Cell>>.From(result) :
                Maybe<IEnumerable<Cell>>.None;
        }
    }
}
