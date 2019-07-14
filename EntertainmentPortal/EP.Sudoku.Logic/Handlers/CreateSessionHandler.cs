using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Sudoku.Data.Context;
using EP.Sudoku.Data.Models;
using EP.Sudoku.Logic.Models;
using EP.Sudoku.Logic.Commands;
using EP.Sudoku.Logic.Enums;
using EP.Sudoku.Logic.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EP.Sudoku.Logic.Handlers
{
    public class CreateSessionHandler : IRequestHandler<CreateSessionCommand, Result<Session>>
    {
        private readonly SudokuDbContext _context;
        private readonly IMapper _mapper;

        public CreateSessionHandler(SudokuDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<Session>> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
        {
            /*var sessionDb = _mapper.Map<SessionDb>(request);*/ //не работает в тестах, а так работает
            var sessionDb = new SessionDb
            {
                Level = (int)request.Level,
                Hint = request.Hint,
                IsOver = request.IsOver,
                PlayerDbId = request.PlayerId
            };

            if (request.PlayerId == 0)
            {
                sessionDb.PlayerDbId = 1;
            }
            RemoveSessionIfExists(sessionDb.PlayerDbId, cancellationToken);
            GenerationSudokuService sudokuService = new GenerationSudokuService();
            List<Cell> cells = sudokuService.GetSudoku((DifficultyLevel)sessionDb.Level);         
            sessionDb.SquaresDb = _mapper.Map<List<CellDb>>(cells);
            sessionDb.Hint = 3;
            sessionDb.IsOver = false;
            _context.Add(sessionDb);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Ok<Session>(_mapper.Map<Session>(sessionDb));
        }

        public async void RemoveSessionIfExists(long id, CancellationToken cancellationToken)
        {
            var player = _context.Players
                .Include(p => p.GameSessionDb)
                .Where(x => x.Id == id)
                .Select(b => b).FirstOrDefault();
            if (player != null)
            {
                if (player.GameSessionDb != null)
                {
                    _context.Remove(player.GameSessionDb);
                    await _context.SaveChangesAsync(cancellationToken);
                }
            }
        }
    }
}
