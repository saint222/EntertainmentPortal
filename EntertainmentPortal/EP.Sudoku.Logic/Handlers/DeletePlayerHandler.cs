using AutoMapper;
using EP.Sudoku.Data;
using EP.Sudoku.Data.Models;
using EP.Sudoku.Logic.Models;
using EP.Sudoku.Logic.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using EP.Sudoku.Logic.Commands;
using EP.Sudoku.Data.Context;

namespace EP.Sudoku.Logic.Handlers
{
    public class DeletePlayerHandler : IRequestHandler<DeletePlayerCommand, bool>
    {
        private readonly SudokuDbContext _context;        
        public DeletePlayerHandler(SudokuDbContext context)
        {
            _context = context;            
        }
        public async Task<bool> Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
        {
            var deletedPlayer = _context.Players.Where(x => x.Id == request.Id).FirstOrDefault();
            if (deletedPlayer == null)
            {
                return await Task.FromResult(false);
            }
            _context.Remove(deletedPlayer);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return await Task.FromResult(true);
        }
    }
}
