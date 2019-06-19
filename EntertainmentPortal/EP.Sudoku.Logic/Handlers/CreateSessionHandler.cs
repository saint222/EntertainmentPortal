using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EP.Sudoku.Data.Context;
using EP.Sudoku.Data.Models;
using EP.Sudoku.Logic.Models;
using EP.Sudoku.Logic.Commands;
using EP.Sudoku.Logic.Enums;
using EP.Sudoku.Logic.Services;
using MediatR;

namespace EP.Sudoku.Logic.Handlers
{
    public class CreateSessionHandler : IRequestHandler<CreateSessionCommand, Session>
    {
        private readonly SudokuDbContext _context;
        private readonly IMapper _mapper;

        public CreateSessionHandler(SudokuDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Session> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
        {
            var sessionDb = _mapper.Map<SessionDb>(request.session);
            sessionDb.ParticipantDb = _context.Find<PlayerDb>(request.session.Participant.Id);
            GenerationSudokuService sudokuService = new GenerationSudokuService();
            List<Cell> cells = sudokuService.GetSudoku((DifficultyLevel)sessionDb.Level);         
            sessionDb.SquaresDb = _mapper.Map<List<CellDb>>(cells);
            _context.Add(sessionDb);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return await Task.FromResult(request.session);
        }
    }
}
