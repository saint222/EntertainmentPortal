using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EP.Sudoku.Data.Context;
using EP.Sudoku.Logic.Models;
using EP.Sudoku.Logic.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EP.Sudoku.Logic.Handlers
{
    public class GetSessionByIdHandler : IRequestHandler<GetSessionById, Session>
    {
        private readonly SudokuDbContext _context;
        private readonly IMapper _mapper;

        public GetSessionByIdHandler(SudokuDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Session> Handle(GetSessionById request, CancellationToken cancellationToken)
        {
            var chosenSession = _context.Sessions
                .Include(b => b.ParticipantDb)
                .ThenInclude(a => a.IconDb)
                .Include(c => c.SquaresDb)                
                .Where(x => x.Id == request.Id)
                .Select(d => _mapper.Map<Session>(d)).FirstOrDefault();

            return await Task.FromResult(chosenSession);
        }
    }
}
