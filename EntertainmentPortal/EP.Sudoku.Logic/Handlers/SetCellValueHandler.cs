using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EP.Sudoku.Data.Context;
using EP.Sudoku.Data.Models;
using EP.Sudoku.Logic.Commands;
using EP.Sudoku.Logic.Models;
using EP.Sudoku.Logic.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EP.Sudoku.Logic.Handlers
{
    public class SetCellValueHandler : IRequestHandler<SetCellValueCommand, Session>
    {
        private readonly SudokuDbContext _context;
        private readonly IMapper _mapper;

        public SetCellValueHandler(SudokuDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Session> Handle(SetCellValueCommand request, CancellationToken cancellationToken)
        {
            var session = _context.Sessions
                .Include(d => d.SquaresDb)
                .First(d => d.Id == request.Id);
            session.SquaresDb.Where(x => x.X == request.X && x.Y == request.Y).FirstOrDefault().Value = request.Value;

            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return await Task.FromResult(_mapper.Map<Session>(session));
        }
    }
}
