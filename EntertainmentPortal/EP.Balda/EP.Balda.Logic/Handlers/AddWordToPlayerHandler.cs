using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Balda.Data.Context;
using EP.Balda.Logic.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using EP.Balda.Logic.Models;
using EP.Balda.Data.Models;
using System.Collections.Generic;

namespace EP.Balda.Logic.Handlers
{
    public class AddWordToPlayerHandler : IRequestHandler<AddWordToPlayerCommand, Result<Player>>
    {
        private readonly IMapper _mapper;
        private readonly BaldaGameDbContext _context;

        public AddWordToPlayerHandler(IMapper mapper, BaldaGameDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<Player>> Handle(AddWordToPlayerCommand request, CancellationToken cancellationToken)
        {
            var player = await (_context.Players
                .Where(p => p.Id == request.Id)
                .FirstOrDefaultAsync<PlayerDb>())
                .ConfigureAwait(false);

            if (player == null)
                return Result.Fail<Player>($"There is no player's id {request.Id} in database");

            var playerGame = _context.PlayerGames.Where(p => p.PlayerId == request.Id && p.GameId == request.GameId);

            if(playerGame == null)
                return Result.Fail<Player>($"There is no player's id {request.Id} or game's {request.GameId} in database");

            var wordRu = _context.WordsRu.SingleOrDefault(w => w.Word == request.Word);

            if (wordRu == null)
                return Result.Fail<Player>($"There is no word {request.Word} in word database");

            var playerWordDb = new PlayerWord()
            {
                PlayerId = request.Id,
                WordId = wordRu.Id,
                GameId = request.GameId
            };

            //TODO Add when create player
            player.PlayerWords = new List<PlayerWord>();
            player.PlayerWords.Add(playerWordDb);

            try
            {
                await _context.SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);

                return Result.Ok(_mapper.Map<Player>(player));
            }
            catch (DbUpdateException ex)
            {
                return Result.Fail<Player>(ex.Message);
            }
        }
    }
}