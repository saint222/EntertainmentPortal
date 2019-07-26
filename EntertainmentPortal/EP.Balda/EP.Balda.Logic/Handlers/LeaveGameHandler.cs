using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Balda.Data.Context;
using EP.Balda.Logic.Commands;
using EP.Balda.Logic.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EP.Balda.Logic.Handlers
{
    public class LeaveGameHandler : IRequestHandler<LeaveGameCommand, Result<Game>>
    {
        private readonly BaldaGameDbContext _context;
        private readonly IMapper _mapper;

        public LeaveGameHandler(BaldaGameDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<Game>> Handle(LeaveGameCommand request, CancellationToken cancellationToken)
        {
            var game = await _context.Games.Where(g => g.Id == request.GameId)
               .FirstOrDefaultAsync(cancellationToken);

            game.IsGameOver = true;
            _context.Entry(game).State = EntityState.Deleted;

            try
            {
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Ok(_mapper.Map<Game>(game));
            }
            catch (DbUpdateException ex)
            {
                return Result.Fail<Game>(ex.Message);
            }
        }
    }
}
