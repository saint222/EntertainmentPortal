using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Hangman.Data.Models;
using MediatR;
using EP.Hangman.Logic.Models;
using EP.Hangman.Logic.Queries;
using EP.Hangman.Data.Context;
using FluentValidation;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace EP.Hangman.Logic.Handlers
{
    public class GetUserSessionHandler : IRequestHandler<GetUserSession, Result<ControllerData>>
    {
        private readonly GameDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<GetUserSession> _validator;
        private readonly ILogger<GetUserSessionHandler> _logger;
        private readonly IMemoryCache _cache;
        private const string KEY = "CashedGamesSessions";

        public GetUserSessionHandler(GameDbContext context, IMapper mapper, IValidator<GetUserSession> validator, ILogger<GetUserSessionHandler> logger, IMemoryCache cache)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
            _cache = cache;
        }

        public async Task<Result<ControllerData>> Handle(GetUserSession request, CancellationToken cancellationToken)
        {
            var validator = _validator.Validate(request);
            if (!validator.IsValid)
            {
                _logger.LogError("Request is invalid");
                return Result.Fail<ControllerData>(validator.Errors.First().ErrorMessage);
            }

            var items = _cache.Get<IEnumerable<GameDb>>(KEY);
            if (items?.Where(item => item.Id == request._data.Id) != null)
            {
                return Result.Ok<ControllerData>(_mapper.Map<GameDb, ControllerData>(items.FirstOrDefault(item => item.Id == request._data.Id)));
            }

            var result = _mapper.Map<UserGameData, ControllerData>(_mapper.Map<GameDb, UserGameData>(await _context.Games.FindAsync(request._data.Id)));
            _cache.Set(KEY, result, DateTimeOffset.Now.AddMinutes(5));
            if (result != null)
            {
                return Result.Ok<ControllerData>(result);
            }
            else
            {
                _logger.LogError($"Game session {request._data.Id} wasn't found");
                return Result.Fail<ControllerData>("Session not found");
            }
        }
    }
}
