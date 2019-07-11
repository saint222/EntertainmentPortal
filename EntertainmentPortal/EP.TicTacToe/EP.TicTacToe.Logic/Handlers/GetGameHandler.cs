using System;
using System.Collections.Generic;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.TicTacToe.Data.Context;
using EP.TicTacToe.Logic.Models;
using EP.TicTacToe.Logic.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace EP.TicTacToe.Logic.Handlers
{
    public class GetGameHandler : IRequestHandler<GetGame, Maybe<Game>>
    {
        private readonly TicTacDbContext _context;
        private readonly IMapper _mapper;

        public GetGameHandler(TicTacDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Maybe<Game>> Handle(GetGame request,
                                              CancellationToken cancellationToken)
        {
            var gameDb = await _context.Games
                .Where(g => g.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            var mapDb = await _context.Maps
                .Where(c => c.GameId == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            var playerOne = _context.Players
                                .Where(c => c.GameId == request.Id)
                                .Select(s => s.FirstPlayer)
                                .Select(x => x.HaunterId)
                                .FirstOrDefaultAsync(cancellationToken)
                                .Result;

            var playerTwo = _context.Players
                                .Where(c => c.GameId == request.Id)
                                .Select(s => s.SecondPlayer)
                                .Select(x=>x.HaunterId)
                                .FirstOrDefaultAsync(cancellationToken)
                                .Result;

            var cells = await _context.Cells.Where(c => c.MapId == mapDb.Id)
                .ToListAsync(cancellationToken);

            var cellList = new List<int>();
            foreach (var item in cells) cellList.Add(item.TicTac);

            var gameDto = new Game
            {
                Id = request.Id,
                MapId = mapDb.Id,
                PlayerOne = Convert.ToInt32(playerOne),
                PlayerTwo = Convert.ToInt32(playerTwo),
                TicTacList = cellList
            };


            return gameDb == null
                ? Maybe<Game>.None
                : Maybe<Game>.From(_mapper.Map<Game>(gameDto));
        }
    }
}