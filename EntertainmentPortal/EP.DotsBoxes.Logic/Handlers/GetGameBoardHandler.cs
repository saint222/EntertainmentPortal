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
using EP.DotsBoxes.Logic.Models;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace EP.DotsBoxes.Logic.Handlers
{
    public class GetGameBoardHandler : IRequestHandler<GetGameBoard, Maybe<IEnumerable<GameBoard>>>
    {
        private readonly IMapper _mapper;
        private readonly GameBoardDbContext _context;
        private readonly IMemoryCache _cache;
        private const string KEY = "CachedGameBoard";

        public GetGameBoardHandler(IMapper mapper, GameBoardDbContext context, IMemoryCache cache)
        {
            _mapper = mapper;
            _context = context;
            _cache = cache;
        }

        public async Task<Maybe<IEnumerable<GameBoard>>> Handle(GetGameBoard request, CancellationToken cancellationToken)
        {
            var items = _cache.Get<IEnumerable<GameBoard>>(KEY);
            if (items != null)
            {
                return Maybe<IEnumerable<GameBoard>>.From(items);
            }

            var gameBoard = await _context.GameBoard
                .Include(b => b.Cells)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
                        
            var result = _mapper.Map<List<GameBoardDb>, IEnumerable<GameBoard>>(gameBoard);
            _cache.Set(KEY, result, DateTimeOffset.Now.AddSeconds(10));

            return result.Any() ?
                Maybe<IEnumerable<GameBoard>>.From(result) :
                Maybe<IEnumerable<GameBoard>>.None;
        }
    }
}
