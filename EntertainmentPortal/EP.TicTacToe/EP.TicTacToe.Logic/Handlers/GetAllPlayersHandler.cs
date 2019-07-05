using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.TicTacToe.Data.Context;
using EP.TicTacToe.Logic.Models;
using EP.TicTacToe.Logic.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace EP.TicTacToe.Logic.Handlers
{
    public class
        GetAllPlayersHandler : IRequestHandler<GetAllPlayers, Maybe<IEnumerable<Player>>>
    {
        private readonly TicTacDbContext _context;
        private readonly IMapper _mapper;

        public GetAllPlayersHandler(IMapper mapper, TicTacDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Maybe<IEnumerable<Player>>> Handle(
            GetAllPlayers request, CancellationToken cancellationToken)
        {
            var playersDb = await _context.Players
                .ToArrayAsync(cancellationToken);

            return playersDb.Any()
                ? Maybe<IEnumerable<Player>>.From(
                    _mapper.Map<IEnumerable<Player>>(playersDb))
                : Maybe<IEnumerable<Player>>.None;
        }
    }
}