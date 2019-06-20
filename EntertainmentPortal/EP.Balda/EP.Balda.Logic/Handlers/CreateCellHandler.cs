using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Balda.Data.Context;
using EP.Balda.Logic.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using EP.Balda.Logic.Models;
using EP.Balda.Data.Models;

namespace EP.Balda.Logic.Handlers
{
    public class CreateCellHandler : IRequestHandler<CreateCellCommand, Result<Cell>>
    {
        private readonly IMapper _mapper;
        private readonly BaldaGameDbContext _context;

        public CreateCellHandler(IMapper mapper, BaldaGameDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<Cell>> Handle(CreateCellCommand request, CancellationToken cancellationToken)
        {
            CellDb cellDb = new CellDb
            {
                Letter = request.Letter,
                Map = new MapDb(),
                MapId = 1,
                X = request.X,
                Y = request.Y
            };

            _context.Cells.Add(cellDb);

            try
            {
                
                await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                return Result.Ok(_mapper.Map<Cell>(cellDb));
            }
            catch (DbUpdateException ex)
            {
                return Result.Fail<Cell>(ex.Message);
            }
        }
    }
}