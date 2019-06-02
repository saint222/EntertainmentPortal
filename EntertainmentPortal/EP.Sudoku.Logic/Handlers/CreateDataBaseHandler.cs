using EP.Sudoku.Data.Context;
using EP.Sudoku.Logic.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EP.Sudoku.Logic.Handlers
{
    public class CreateDataBaseHandler : AsyncRequestHandler<CreateDatabaseCommand>
    {
        private readonly SudokuDbContext _context;

        public CreateDataBaseHandler(SudokuDbContext context)
        {
            _context = context;
        }

        protected override async Task Handle(CreateDatabaseCommand request, CancellationToken cancellationToken)
        {
            await _context.Database.EnsureCreatedAsync(cancellationToken);
        }
    }
}
