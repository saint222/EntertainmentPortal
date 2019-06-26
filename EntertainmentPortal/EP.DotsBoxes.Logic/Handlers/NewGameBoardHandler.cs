using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.DotsBoxes.Data.Context;
using EP.DotsBoxes.Data.Models;
using EP.DotsBoxes.Logic.Commands;
using EP.DotsBoxes.Logic.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EP.DotsBoxes.Logic.Handlers
{
    public class NewGameBoardHandler : IRequestHandler<NewGameBoardCommand, Result<GameBoard>>
    {
        private readonly IMapper _mapper;
        private readonly GameBoardDbContext _context;
        private readonly IValidator<NewGameBoardCommand> _validator;
        private readonly ILogger<NewGameBoardHandler> _logger;

        public NewGameBoardHandler(IMapper mapper, GameBoardDbContext context, IValidator<NewGameBoardCommand> validator, ILogger<NewGameBoardHandler> logger)
        {
            _mapper = mapper;
            _context = context;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<GameBoard>> Handle(NewGameBoardCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Adding a new game board.");
            //validate
            var result = _validator.Validate(request);

            if (result == null)
            {
                _logger.LogError($"Result: {result.ToString()}!");
                return Result.Fail<GameBoard>(result.Errors.First().ErrorMessage);
            }

            var model = new GameBoard()
            {
                Rows = request.Rows,
                Columns = request.Columns,
                Cells = new GameLogic().CreateGameBoard(request.Rows,request.Columns)
            };

           _context.GameBoard.Add(_mapper.Map<GameBoardDb>(model));
            
            try
            {
                _logger.LogInformation("Updating database with a new game board.");
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Ok<GameBoard>(_mapper.Map<GameBoard>(model));
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message, "Unsuccessful database update with a new game board!");
                return Result.Fail<GameBoard>(ex.Message);
            }
        }
    }
}
