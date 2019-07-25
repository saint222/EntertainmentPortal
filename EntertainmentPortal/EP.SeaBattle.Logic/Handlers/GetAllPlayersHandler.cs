using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EP.SeaBattle.Data.Context;
using EP.SeaBattle.Logic.Queries;
using EP.SeaBattle.Logic.Models;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;

namespace EP.SeaBattle.Logic.Handlers
{
    public class GetAllPlayersHandler : IRequestHandler<GetAllPlayersQuery, Maybe<IEnumerable<Player>>>
    {
        private readonly SeaBattleDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private const string KEY = "CachedPlayers";

        public GetAllPlayersHandler(SeaBattleDbContext context, IMapper mapper, IMemoryCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Maybe<IEnumerable<Player>>> Handle(GetAllPlayersQuery request, CancellationToken cancellationToken)
        {
            var items = _cache.Get<IEnumerable<Player>>(KEY);
            if (items != null)
            {
                return Maybe <IEnumerable<Player>>.From(items);
            }

            var result = _mapper.Map<IEnumerable<Player>>(await _context.Players
                                                                    .Include(p => p.Ships)
                                                                    .AsNoTracking()
                                                                    .ToArrayAsync(cancellationToken)
                                                                    .ConfigureAwait(false));
            _cache.Set(KEY, result, DateTimeOffset.Now.AddMinutes(5));

            return !result.Any() ?
                Maybe<IEnumerable<Player>>.None :
                Maybe<IEnumerable<Player>>.From(result);
        }
    }
}
