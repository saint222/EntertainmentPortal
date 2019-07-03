using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.WordsMaker.Data.Context;
using EP.WordsMaker.Data.Models;
using EP.WordsMaker.Logic.Commands;
using EP.WordsMaker.Logic.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EP.WordsMaker.Logic.Handlers
{
    public class DeletePlayerHandler : IRequestHandler<DeletePlayerCommand, Result<Player>>
    {
        private readonly GameDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<DeletePlayerCommand> _validator;

        public DeletePlayerHandler(GameDbContext context, IMapper mapper, IValidator<DeletePlayerCommand> validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Result<Player>> Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
        {
            var validator = _validator.Validate(request);

            if (!validator.IsValid)
            {
                return Result.Fail<Player>(validator.Errors.First().ErrorMessage);
            }

            var result = await _context.Players.FindAsync(request.Id);
            if (result == null)
            {
                return Result.Fail<Player>("Data wasn't found");
            }

            _context.Entry<PlayerDb>(result).State = EntityState.Deleted;

            try
            {
                await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                return Result.Ok<Player>(null);
            }
            catch (DbUpdateException exception)
            {
                return Result.Fail<Player>(exception.Message);
            }
        }
    }
}