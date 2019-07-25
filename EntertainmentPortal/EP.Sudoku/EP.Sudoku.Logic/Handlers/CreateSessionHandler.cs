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
using Microsoft.Extensions.Logging;

namespace EP.Sudoku.Logic.Handlers
{
    public class CreateSessionHandler : IRequestHandler<CreateSessionCommand, Result<Session>>
    {
        private readonly SudokuDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateSessionHandler> _logger;

        public CreateSessionHandler(SudokuDbContext context, IMapper mapper, ILogger<CreateSessionHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<Session>> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
        {
            var sessionDb = _mapper.Map <SessionDb>(request); //не работает в тестах, а так работает
            var player = _context.Players.FirstOrDefault(x => x.UserId == request.UserId);
            if (player == null)
            {
                _logger.LogError($"Player not found");
                return Result.Fail<Session>("Player not found");
            }

            sessionDb.PlayerDbId = player.Id;

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
