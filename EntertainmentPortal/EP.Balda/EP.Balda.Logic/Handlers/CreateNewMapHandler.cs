using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Balda.Data.Context;
using EP.Balda.Data.Models;
using EP.Balda.Logic.Commands;
using EP.Balda.Logic.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EP.Balda.Logic.Handlers
{
    public class
        CreateNewMapHandler : IRequestHandler<CreateNewMapCommand, Result<Map>>
    {
        private readonly BaldaGameDbContext _context;
        private readonly IMapper _mapper;

        public CreateNewMapHandler(BaldaGameDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<Map>> Handle(CreateNewMapCommand request,
                                                 CancellationToken cancellationToken)
        {
            var model = new MapDb();

            _context.Maps.Add(model);

            try
            {
                await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                return Result.Ok(_mapper.Map<Map>(model));
            }
            catch (DbUpdateException ex)
            {
                return Result.Fail<Map>(ex.Message);
            }
        }
    }
}