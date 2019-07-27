using AutoMapper;
using CSharpFunctionalExtensions;
using EP.SeaBattle.Data.Context;
using EP.SeaBattle.Logic.Commands;
using EP.SeaBattle.Logic.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EP.SeaBattle.Logic.Handlers
{
    public class GetPlayerHandler : IRequestHandler<GetPlayerQuery, Result<Player>>
    {
        private readonly SeaBattleDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GetPlayerHandler(SeaBattleDbContext context, IMapper mapper, ILogger<GetPlayerHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<Player>> Handle(GetPlayerQuery request, CancellationToken cancellationToken)
        {          
            try
            {
                var player = await _context.Players
                                            .FirstOrDefaultAsync(p => p.UserId == request.UserId)
                                            .ConfigureAwait(false);
                if (player == null)
                {
                    _logger.LogInformation($"Player with id {request.UserId} not found");
                    return Result.Fail<Player>("Player not found");
                }
                else
                {
                    _logger.LogInformation($"Player with id {request.UserId} founded");
                    return Result.Ok(_mapper.Map<Player>(player));
                }               
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message);
                return Result.Fail<Player>($"Cannot find player id {request.UserId}");
            }
        }
    }
}
