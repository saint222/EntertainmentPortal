using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EP.DotsBoxes.Logic.Models;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.DotsBoxes.Data.Context;
using EP.DotsBoxes.Logic.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EP.DotsBoxes.Logic.Handlers
{
    public class UpdateGameBoardHandler : IRequestHandler<UpdateGameBoardCommand, Result<Cell>>
    {
        private readonly IMapper _mapper;
        private readonly GameBoardDbContext _context;
        private readonly ILogger<UpdateGameBoardCommand> _logger;

        public UpdateGameBoardHandler(IMapper mapper, GameBoardDbContext context, ILogger<UpdateGameBoardCommand> logger)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }

        public async Task<Result<Cell>> Handle(UpdateGameBoardCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating the game board.");
           
            var model = new Cell()
            {
                Row = request.Row,
                Column = request.Column,
                Top = request.Top,
                Bottom = request.Bottom,
                Left = request.Left,
                Right = request.Right
            };

            var context = _context.Cells
                .FirstOrDefault(c => c.Row == request.Row && c.Column == request.Column);
            context.Top = request.Top;
            context.Bottom = request.Bottom;
            context.Left = request.Left;
            context.Right = request.Right;
           _context.Entry(context).State = EntityState.Modified;
           
            try
            {
                _logger.LogInformation("Updating database the game board.");
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Ok<Cell>(model);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message, "Unsuccessful database update the game board!");
                return Result.Fail<Cell>(ex.Message);
            }
        }
    }
}
