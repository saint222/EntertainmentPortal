using AutoMapper;
using CSharpFunctionalExtensions;
using EP.DotsBoxes.Data.Context;
using EP.DotsBoxes.Data.Models;
using EP.DotsBoxes.Logic.Models;
using EP.DotsBoxes.Logic.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var result = await _context.Cells
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            IEnumerable<Cell> cells = _mapper.Map<List<CellDb>, IEnumerable<Cell>>(result);

            return result.Any() ?
                Maybe<IEnumerable<Cell>>.From(cells) :
                Maybe<IEnumerable<Cell>>.None;
        }
    }
}
