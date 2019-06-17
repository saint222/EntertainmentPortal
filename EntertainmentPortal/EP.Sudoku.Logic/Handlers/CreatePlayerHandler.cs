using AutoMapper;
using EP.Sudoku.Data.Context;
using EP.Sudoku.Data.Models;
using EP.Sudoku.Logic.Commands;
using EP.Sudoku.Logic.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EP.Sudoku.Logic.Handlers
{
    public class CreatePlayerHandler : IRequestHandler<CreatePlayerCommand, Player>
    {
        private readonly SudokuDbContext _context;
        private readonly IMapper _mapper;
        public CreatePlayerHandler(SudokuDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Player> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
        {            
            var playerDb = _mapper.Map<PlayerDb>(request.player);
            playerDb.IconDb = _context.Find<AvatarIconDb>(request.player.Icon.Id);           
            //playerDb.GameSessionsDb = _mapper.Map<List<SessionDb>>(request.player.GameSessions);
            _context.Add(playerDb);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return await Task.FromResult(request.player);
        }
    }
}
