using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Sudoku.Data.Context;
using EP.Sudoku.Logic.Models;
using EP.Sudoku.Logic.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace EP.Sudoku.Logic.Handlers
{
    public class GetPlayerByUserIdHandler : IRequestHandler<GetPlayerByUserId, Maybe<Player>>
    {
        private readonly SudokuDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<GetPlayerByIdHandler> _logger;

        public GetPlayerByUserIdHandler(SudokuDbContext context, IMapper mapper, IMemoryCache memoryCache, ILogger<GetPlayerByIdHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        public async Task<Maybe<Player>> Handle(GetPlayerByUserId request, CancellationToken cancellationToken)
        {
                var result = await _context.Players
                    .Include(p => p.IconDb)
                    .Include(p => p.GameSessionDb)
                    .Where(x => x.UserId == request.Id)
                    .Select(b => _mapper.Map<Player>(b))
                    .FirstOrDefaultAsync();

            return result != null ?
                Maybe<Player>.From(result) :
                Maybe<Player>.None; 
        }
    }
}
