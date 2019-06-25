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
            var result = await _validator
                .ValidateAsync(request, ruleSet: "PlayerDbNotExistingSet", cancellationToken: cancellationToken);

            if (!result.IsValid)
            {
                return Result.Fail<Player>(result.Errors.First().ErrorMessage);
            }

            var playerDb = await _context.Players
                .Where(p => p.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

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