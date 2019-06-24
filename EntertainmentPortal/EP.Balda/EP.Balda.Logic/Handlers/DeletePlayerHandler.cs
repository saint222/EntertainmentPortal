using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Balda.Data.Context;
using EP.Balda.Logic.Commands;
using EP.Balda.Logic.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EP.Balda.Logic.Handlers
{
    public class
        DeletePlayerHandler : IRequestHandler<DeletePlayerCommand, Result<Player>>
    {
        private readonly BaldaGameDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<DeletePlayerCommand> _validator;

        public DeletePlayerHandler(IMapper mapper, BaldaGameDbContext context,
                                   IValidator<DeletePlayerCommand> validator)
        {
            _mapper = mapper;
            _context = context;
            _validator = validator;
        }

        public async Task<Result<Player>> Handle(DeletePlayerCommand request,
                                                 CancellationToken cancellationToken)
        {
            var playerDb = await _context.Players
                .Where(p => p.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            var validator = _validator.Validate(request);

            if (!validator.IsValid)
            {
                return Result.Fail<Player>(validator.Errors.First().ErrorMessage);
            }

            _context.Players.Remove(playerDb);

            try
            {
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Ok(_mapper.Map<Player>(playerDb));
            }
            catch (DbUpdateException ex)
            {
                return Result.Fail<Player>(ex.Message);
            }
        }
    }
}