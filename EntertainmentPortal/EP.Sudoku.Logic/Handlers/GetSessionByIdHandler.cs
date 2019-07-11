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
using Microsoft.Extensions.Logging;
using Remotion.Linq.Clauses;

namespace EP.Sudoku.Logic.Handlers
{
    public class GetSessionByIdHandler : IRequestHandler<GetSessionById, Session>
    {
        private readonly SudokuDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetSessionByIdHandler> _logger;

        public GetSessionByIdHandler(SudokuDbContext context, IMapper mapper, ILogger<GetSessionByIdHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Session> Handle(GetSessionById request, CancellationToken cancellationToken)
        {
            var chosenSession = _context.Sessions                
                .Include(c => c.SquaresDb)               
                .Where(x => x.Id == request.Id)
                .Select(d => _mapper.Map<Session>(d)).FirstOrDefault();

            if (chosenSession != null)
            {
                chosenSession.Squares = chosenSession.Squares.OrderBy(c => c.X).ThenBy(c => c.Y).ToList();
            }
            else
            {
                _logger.LogError($"There is not a gamesession with the Id '{request.Id}'...");
            }

            return await Task.FromResult(chosenSession);
        }
    }
}
