using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Balda.Data.Context;
using EP.Balda.Logic.Models;
using EP.Balda.Logic.Queries;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EP.Balda.Logic.Handlers
{
    public class GetPlayerHandler : IRequestHandler<GetPlayer, Maybe<Player>>
    {
        private readonly BaldaGameDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<GetPlayer> _validator;

        public GetPlayerHandler(IMapper mapper, BaldaGameDbContext context, 
            IValidator<GetPlayer> validator)
        {
            _mapper = mapper;
            _context = context;
            _validator = validator;
        }

        public async Task<Maybe<Player>> Handle(GetPlayer request,
                                                CancellationToken cancellationToken)
        {
            var result = await _validator
                .ValidateAsync(request, ruleSet: "PlayerExistingSet", cancellationToken: cancellationToken);

            if (!result.IsValid)
            {
                return Maybe<Player>.None;
            }

            var playerDb = await _context.Players
                .Where(p => p.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            return playerDb == null
                ? Maybe<Player>.None
                : Maybe<Player>.From(_mapper.Map<Player>(playerDb));
        }
    }
}