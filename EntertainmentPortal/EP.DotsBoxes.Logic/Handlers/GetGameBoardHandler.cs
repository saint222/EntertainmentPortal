using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EP.DotsBoxes.Data;
using EP.DotsBoxes.Data.Context;
using EP.DotsBoxes.Logic.Models;
using EP.DotsBoxes.Logic.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EP.DotsBoxes.Logic.Handlers
{
    public class GetGameBoardHandler : IRequestHandler<GetGameBoard, IEnumerable<GameBoard>>
    {
        private readonly IMapper _mapper;
        private readonly GameBoardDbContext _context;

        public GetGameBoardHandler(IMapper mapper, GameBoardDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<GameBoard>> Handle(GetGameBoard request, CancellationToken cancellationToken)
        {
            return await _context.GameBoard
                 .Select(b => _mapper.Map<GameBoard>(b))
                 .ToArrayAsync(cancellationToken)
                 .ConfigureAwait(false);
        }
    }
}
