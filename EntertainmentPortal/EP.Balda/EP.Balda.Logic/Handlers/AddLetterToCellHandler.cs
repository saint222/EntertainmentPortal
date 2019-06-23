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
    public class AddLetterToCellHandler : IRequestHandler<AddLetterToCellCommand, Result<Cell>>
    {
        private readonly IMapper _mapper;
        private readonly BaldaGameDbContext _context;

        public AddLetterToCellHandler(IMapper mapper, BaldaGameDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<Cell>> Handle(AddLetterToCellCommand request, CancellationToken cancellationToken)
        {
            var result = await (_context.Cells
                .Where(c => c.Id == request.Id)
                .FirstOrDefaultAsync<CellDb>());
                
            if(result == null)
                return Result.Fail<Cell>($"There is no cell with id {request.Id} in database");

            if(result.Letter == null)
            {
                result.Letter = request.Letter;
            }
            else
            {
                return Result.Fail<Cell>("Cell already contains letter");
            }

            try
            {
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Ok(_mapper.Map<Cell>(result));
            }
            catch (DbUpdateException ex)
            {
                return Result.Fail<Cell>(ex.Message);
            }
        }
    }
}