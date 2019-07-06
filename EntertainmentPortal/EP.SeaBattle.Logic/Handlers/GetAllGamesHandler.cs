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


namespace EP.SeaBattle.Logic.Handlers
{
    class GetAllGamesHandler : IRequestHandler<GetAllGamesQuery, Maybe<IEnumerable<Game>>>
    {
        private readonly SeaBattleDbContext _context;
        private readonly IMapper _mapper;

        public GetAllGamesHandler(SeaBattleDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Maybe<IEnumerable<Game>>> Handle(GetAllGamesQuery request, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<IEnumerable<Game>>(await _context.Games
                                                                      .Include(g => g.Players)
                                                                      .AsNoTracking()
                                                                      .ToArrayAsync(cancellationToken)
                                                                      .ConfigureAwait(false));

            return !result.Any() ?
                Maybe<IEnumerable<Game>>.None :
                Maybe<IEnumerable<Game>>.From(result);
        }
    }
}