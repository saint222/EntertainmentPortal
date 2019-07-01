using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Balda.Data.Context;
using EP.Balda.Logic.Models;
using EP.Balda.Logic.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EP.Balda.Logic.Handlers
{
    public class
        GetAllPlayersHandler : IRequestHandler<GetAllPlayers, Maybe<IEnumerable<Player>>>
    {
        private readonly BaldaGameDbContext _context;
        private readonly IMapper _mapper;

        public GetAllPlayersHandler(BaldaGameDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Maybe<IEnumerable<Player>>> Handle(
            GetAllPlayers request, CancellationToken cancellationToken)
        {
            var playersDb = await _context.Users
                .ToArrayAsync(cancellationToken);

            return playersDb.Any()
                ? Maybe<IEnumerable<Player>>.From(
                    _mapper.Map<IEnumerable<Player>>(playersDb))
                : Maybe<IEnumerable<Player>>.None;
        }
    }
}