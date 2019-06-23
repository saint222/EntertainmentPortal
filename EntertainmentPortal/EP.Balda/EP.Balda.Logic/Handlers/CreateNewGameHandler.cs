using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Balda.Data.Context;
using EP.Balda.Data.Models;
using EP.Balda.Logic.Commands;
using EP.Balda.Logic.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EP.Balda.Logic.Handlers
{
    public class CreateNewGameHandler : IRequestHandler<CreateNewGameCommand, Result<Game>>
    {
        private readonly BaldaGameDbContext _context;
        private readonly IMapper _mapper;
        
        public CreateNewGameHandler(BaldaGameDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<Game>> Handle(CreateNewGameCommand request, CancellationToken cancellationToken)
        {
            //TODO: add cells initializator
            var player = await (_context.Players
                .Where(p => p.Id == request.PlayerId)
                .FirstOrDefaultAsync<PlayerDb>());

            if (player == null)
                return Result.Fail<Game>($"There is no player's id {request.PlayerId} in database");

            var map = new Map();
            var mapDb = _mapper.Map<MapDb>(map);
            _context.Maps.Add(mapDb);

            var gameDb = new GameDb()
            {
                Map = mapDb,
                MapId = mapDb.Id,
            };

            _context.Games.Add(gameDb);

            gameDb.PlayerGames = new List<PlayerGame>();

            var playerGame = new PlayerGame
            {
                GameId = gameDb.Id,
                PlayerId = request.PlayerId
            };

            gameDb.PlayerGames.Add(playerGame);

            try
            {
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Ok(_mapper.Map<Game>(gameDb));
            }
            catch (DbUpdateException ex)
            {
                return Result.Fail<Game>(ex.Message);
            }
        }
    }
}