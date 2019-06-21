using System;
using System.Collections.Generic;
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
    public class UpdateCellHandler : IRequestHandler<UpdateCellCommand, Cell>
    {
        private readonly SudokuDbContext _context;
        private readonly IMapper _mapper;

        public UpdateCellHandler(SudokuDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Cell> Handle(UpdateCellCommand request, CancellationToken cancellationToken)
        {
            var cellDb = _mapper.Map<CellDb>(request.cell);
            _context.Entry(cellDb).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(_mapper.Map<Cell>(cellDb));
        }
    }
}
