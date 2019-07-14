using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EP.SeaBattle.Data.Context;
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
    public class GetShipsHandler : IRequestHandler<GetShipsQuery, Maybe<IEnumerable<Ship>>>
    {
        private readonly SeaBattleDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetShipsQuery> _logger;

        public GetShipsHandler(SeaBattleDbContext context, IMapper mapper, ILogger<GetShipsQuery> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Maybe<IEnumerable<Ship>>> Handle(GetShipsQuery request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(request.PlayerId))
            {

                var result = await _context.Ships.Where(w => w.Player.Id == request.PlayerId)
                    .Include(i => i.Cells)
                    .Include(i => i.Player)
                    .ToArrayAsync(cancellationToken).ConfigureAwait(false);

                _logger.LogInformation($"Send ship collection for player {request.PlayerId}");
                return result.Any() ? Maybe<IEnumerable<Ship>>.From(_mapper.Map<IEnumerable<Ship>>(result))
                    : Maybe<IEnumerable<Ship>>.None;
            }
            return Maybe<IEnumerable<Ship>>.None;
        }
    }
}
