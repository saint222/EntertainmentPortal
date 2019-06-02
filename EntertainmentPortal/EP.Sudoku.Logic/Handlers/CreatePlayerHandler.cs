using AutoMapper;
using EP.Sudoku.Data;
using EP.Sudoku.Data.Context;
using EP.Sudoku.Data.Models;
using EP.Sudoku.Logic.Commands;
using EP.Sudoku.Logic.Models;
using EP.Sudoku.Logic.Queries;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EP.Sudoku.Logic.Handlers
{
    public class CreatePlayerHandler : IRequestHandler<CreatePlayerCommand, bool>
    {
        private readonly SudokuDbContext _context;
        private readonly IMapper _mapper;
        public CreatePlayerHandler(SudokuDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
        {            
            var playerDb = _mapper.Map<PlayerDb>(request.player);            
            _context.Add(playerDb);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return await Task.FromResult(true);
        }
    }
}
