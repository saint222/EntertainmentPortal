using AutoMapper;
using EP.Sudoku.Data.Context;
using EP.Sudoku.Data.Models;
using EP.Sudoku.Logic.Commands;
using EP.Sudoku.Logic.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EP.Sudoku.Logic.Handlers
{
    public class UpdateSessionHandler : IRequestHandler<UpdateSessionCommand, Session>
    {
        private readonly SudokuDbContext _context;
        private readonly IMapper _mapper;

        public UpdateSessionHandler(SudokuDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Session> Handle(UpdateSessionCommand request, CancellationToken cancellationToken)
        {
            var sessionDb = _mapper.Map<SessionDb>(request.session);
            sessionDb.SquaresDb = _mapper.Map<List<CellDb>>(request.session.Squares);
            _context.Entry(sessionDb).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(_mapper.Map<Session>(sessionDb));
        }
    }
    
}
