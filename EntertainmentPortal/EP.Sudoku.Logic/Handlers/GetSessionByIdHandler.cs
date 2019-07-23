using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Sudoku.Data.Context;
using EP.Sudoku.Logic.Models;
using EP.Sudoku.Logic.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EP.Sudoku.Logic.Handlers
{
    public class GetSessionByIdHandler : IRequestHandler<GetSessionById, Maybe<Session>>
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

        public async Task<Maybe<Session>> Handle(GetSessionById request, CancellationToken cancellationToken)
        {
            var chosenSession = await _context.Sessions                
                .Include(c => c.SquaresDb)               
                .Where(x => x.Id == request.Id)
                .Select(d => _mapper.Map<Session>(d)).FirstOrDefaultAsync();

            if (chosenSession != null)
            {
                chosenSession.Squares = chosenSession.Squares.OrderBy(c => c.X).ThenBy(c => c.Y).ToList();
            }
            else
            {
                _logger.LogError($"There is not a gamesession with the Id '{request.Id}'...");
            }

            return chosenSession != null ?
                Maybe<Session>.From(chosenSession) :
                Maybe<Session>.None;
        }
    }
}
