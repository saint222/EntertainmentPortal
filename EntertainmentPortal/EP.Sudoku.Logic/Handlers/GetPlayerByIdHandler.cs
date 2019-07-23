using AutoMapper;
using EP.Sudoku.Data.Context;
using EP.Sudoku.Logic.Models;
using EP.Sudoku.Logic.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EP.Sudoku.Logic.Handlers
{
    public class GetPlayerByIdHandler : IRequestHandler<GetPlayerById, Player>
    {
        private readonly SudokuDbContext _context;
        private readonly IMapper _mapper;
        //private readonly IMemoryCache _memoryCache;
        private readonly ILogger<GetPlayerByIdHandler> _logger;

        public GetPlayerByIdHandler(SudokuDbContext context, IMapper mapper, /*IMemoryCache memoryCache,*/ ILogger<GetPlayerByIdHandler> logger)
        {
            _context = context;
            _mapper = mapper;
           // _memoryCache = memoryCache;
            _logger = logger;
        }

        public async Task<Player> Handle(GetPlayerById request, CancellationToken cancellationToken)
        {
            var chosenPlayer = _context.Players
                    .Include(p => p.IconDb)
                    .Include(p => p.GameSessionDb)
                    .Where(x => x.Id == request.Id)
                    .Select(b => _mapper.Map<Player>(b)).FirstOrDefault();

            //THIS PART OF THE CODE IS RUNNABLE, COMMENTED BECAUSE OF CACHE OPTINS IN TESTS 
            /*if (!_memoryCache.TryGetValue(request.Id, out Player chosenPlayer)) //caching "One"
            {
                chosenPlayer = _context.Players
                    .Include(p => p.IconDb)
                    .Include(p => p.GameSessionDb)
                    .Where(x => x.Id == request.Id)
                    .Select(b => _mapper.Map<Player>(b)).FirstOrDefault();

                if (chosenPlayer != null)
                {
                    _memoryCache.Set(chosenPlayer.Id, chosenPlayer,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(10)));
                }
                else
                {
                    _logger.LogError($"There is not a player with the Id '{request.Id}'...");
                }
            }*/

            return await Task.FromResult(chosenPlayer);
        }
    }
}
