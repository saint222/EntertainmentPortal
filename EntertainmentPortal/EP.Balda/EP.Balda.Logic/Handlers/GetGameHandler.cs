﻿using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Balda.Data.Context;
using EP.Balda.Logic.Models;
using EP.Balda.Logic.Queries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EP.Balda.Logic.Handlers
{
    public class GetGameHandler : IRequestHandler<GetGame, Maybe<Game>>
    {
        private readonly IMapper _mapper;
        private readonly BaldaGameDbContext _context;

        public GetGameHandler(IMapper mapper, BaldaGameDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Maybe<Game>> Handle(GetGame request, CancellationToken cancellationToken)
        {
            var result = await _context.Games
                .FindAsync(request.Id)
                .ConfigureAwait(false);

            return result == null?
                Maybe<Game>.None :
                Maybe<Game>.From(_mapper.Map<Game>(result));
        }
    }
}