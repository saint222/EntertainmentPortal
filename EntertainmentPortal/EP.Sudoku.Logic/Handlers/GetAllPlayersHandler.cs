using AutoMapper;
using EP.Sudoku.Data.Context;
using EP.Sudoku.Logic.Models;
using EP.Sudoku.Logic.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EP.Sudoku.Logic.Handlers
{
    public class GetAllPlayersHandler : IRequestHandler<GetAllPlayers, IEnumerable<Player>>
    {
        private readonly SudokuDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private const string KEY = "Players";

        public GetAllPlayersHandler(SudokuDbContext context, IMapper mapper, IMemoryCache memoryCache)
        {
            _context = context;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<Player>> Handle(GetAllPlayers request, CancellationToken cancellationToken)
        {
            var players = _memoryCache.Get<IEnumerable<Player>>(KEY); //caching "All"
            if (players == null)
            {
                players = await _context.Players
                                .Include(p => p.IconDb)
                                .Include(p => p.GameSessionsDb)
                                .Select(b => _mapper.Map<Player>(b))
                                .ToListAsync()
                                .ConfigureAwait(false);
                _memoryCache.Set(KEY, players, new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(30)));
            }                       

            return await Task.FromResult((IEnumerable<Player>)players);
        }
    }
}
