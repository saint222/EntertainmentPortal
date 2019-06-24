using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using EP.Sudoku.Logic.Commands;
using EP.Sudoku.Data.Context;
using Microsoft.EntityFrameworkCore;
using EP.Sudoku.Logic.Models;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace EP.Sudoku.Logic.Handlers
{
    public class DeletePlayerHandler : IRequestHandler<DeletePlayerCommand, bool>
    {
        private readonly SudokuDbContext _context;
        private readonly ILogger<DeletePlayerHandler> _logger;
        
        public DeletePlayerHandler(SudokuDbContext context, ILogger<DeletePlayerHandler> logger)
        {
            _context = context;
            _logger = logger;            
        }
        public async Task<bool> Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
        {
            var deletedPlayer = _context.Players
                    .Include(p => p.GameSessionsDb)
                    .Where(x => x.Id == request.Id)                    
                    .FirstOrDefault();                            

            if (deletedPlayer == null)
            {
                _logger.LogError($"There is not a player with the Id '{request.Id}'...");
                return await Task.FromResult(false);
            }

            _context.Remove(deletedPlayer);
            await _context.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(true);
        }
    }
}
