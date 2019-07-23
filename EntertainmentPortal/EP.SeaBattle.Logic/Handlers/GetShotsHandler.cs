using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EP.SeaBattle.Data.Context;
using EP.SeaBattle.Data.Models;
using EP.SeaBattle.Logic.Queries;
using EP.SeaBattle.Logic.Models;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace EP.SeaBattle.Logic.Handlers
{
    public class GetShotsHandler : IRequestHandler<GetShotsQuery, Maybe<IEnumerable<Shot>>>
    {
        private readonly SeaBattleDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetShipsQuery> _logger;

        public GetShotsHandler(SeaBattleDbContext context, IMapper mapper, ILogger<GetShipsQuery> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Maybe<IEnumerable<Shot>>> Handle(GetShotsQuery request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(request.GameId) && !string.IsNullOrEmpty(request.AnsweredPlayerID))
            {

                var result = await _context.Shots.Where(shot => shot.GameId == request.GameId 
                        && shot.PlayerId != request.AnsweredPlayerID)
                    .ToArrayAsync();

                _logger.LogInformation($"Send opponent shot collection for game {request.GameId} player {request.AnsweredPlayerID}");
                return result.Any() ? Maybe<IEnumerable<Shot>>.From(_mapper.Map<IEnumerable<Shot>>(result))
                    : Maybe<IEnumerable<Shot>>.None;
            }
            return Maybe<IEnumerable<Shot>>.None;
        }
    }
}
