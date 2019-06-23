using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EP.DotsBoxes.Data.Context;
using EP.DotsBoxes.Logic.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using EP.DotsBoxes.Data.Models;
using CSharpFunctionalExtensions;

namespace EP.DotsBoxes.Logic.Handlers
{
    public class GetGameBoardHandler : IRequestHandler<GetGameBoard, Maybe<IEnumerable<CellDb>>>
    {
        private readonly IMapper _mapper;
        private readonly GameBoardDbContext _context;

        public GetGameBoardHandler(IMapper mapper, GameBoardDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Maybe<IEnumerable<CellDb>>> Handle(GetGameBoard request, CancellationToken cancellationToken)
        {
            var result = await _context.Cells
               .AsNoTracking()
               .ToArrayAsync(cancellationToken);

           
            return result.Any() ?
                Maybe<IEnumerable<CellDb>>.From(result) :
                Maybe<IEnumerable<CellDb>>.None;
        }
    }
}
