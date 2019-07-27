using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EP.SeaBattle.Data.Context;
using EP.SeaBattle.Data.Models;
using EP.SeaBattle.Logic.Commands;
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
    public class DeleteShipHandler : IRequestHandler<DeleteShipCommand, Maybe<IEnumerable<Ship>>>
    {
        private readonly SeaBattleDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<DeleteShipCommand> _validator;
        private readonly ILogger<DeleteShipCommand> _logger;

        public DeleteShipHandler(SeaBattleDbContext context, IMapper mapper, IValidator<DeleteShipCommand> validator, ILogger<DeleteShipCommand> logger)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Maybe<IEnumerable<Ship>>> Handle(DeleteShipCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, ruleSet: "DeleteShipValidation", cancellationToken: cancellationToken);
            if (validationResult.IsValid)
            {
                var cell = _context.Ships.SelectMany(c => c.Cells).Include(i => i.Ship).FirstOrDefault(s => s.X == request.X && s.Y == request.Y);
                if (cell != null)
                {
                    var ship = cell.Ship;
                    if (ship == null)
                    {
                        _logger.LogWarning($"Ship with X = {request.X} and Y = {request.Y} Player = {request.PlayerId} Game = {request.GameId} not found");
                        return Maybe<IEnumerable<Ship>>.From(_mapper.Map<IEnumerable<Ship>>
                            (_context.Ships.Where(s => s.Player.Id == request.PlayerId && s.Game.Id == request.GameId).Include(i => i.Cells)));
                    }
                    else
                    {
                        _context.Ships.Remove(ship);
                        try
                        {
                            await _context.SaveChangesAsync(cancellationToken);
                            return Maybe<IEnumerable<Ship>>.From(_mapper.Map<IEnumerable<Ship>>(_context.Ships.Where(s => s.Player.Id == request.PlayerId && s.Game.Id == request.GameId).Include(i => i.Cells)));
                        }
                        catch (DbUpdateException ex)
                        {
                            _logger.LogError(ex.Message);
                            return Maybe<IEnumerable<Ship>>.From(_mapper.Map<IEnumerable<Ship>>(_context.Ships.Where(s => s.Player.Id == request.PlayerId && s.Game.Id == request.GameId).Include(i => i.Cells)));
                        }
                    }
                }
            }
            return Maybe<IEnumerable<Ship>>.From(_mapper.Map<IEnumerable<Ship>>(_context.Ships.Where(s => s.Player.Id == request.PlayerId && s.Game.Id == request.GameId).Include(i => i.Cells)));
        }
    }
}
