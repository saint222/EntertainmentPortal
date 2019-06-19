using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Balda.Data.Context;
using EP.Balda.Data.Models;
using EP.Balda.Logic.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EP.Balda.Logic.Handlers
{
    public class AddLetterToCellHandler : IRequestHandler<AddLetterToCellCommand, Result<CellDb>>
    {
        private readonly IMapper _mapper;
        private readonly BaldaGameDbContext _context;

        public AddLetterToCellHandler(IMapper mapper, BaldaGameDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<CellDb>> Handle(AddLetterToCellCommand request, CancellationToken cancellationToken)
        {
            var result = _context.Cells.SingleOrDefault(c => c.Id == request.Id);

            if(result == null)
            {
                return Result.Fail<CellDb>($"There is no id {request.Id} in database");
            }

            result.Letter = request.Letter;

            try
            {
                
                await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                return Result.Ok(result);
            }
            catch (DbUpdateException ex)
            {
                return Result.Fail<CellDb>(ex.Message);
            }
        }
    }
}