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

namespace EP.Hangman.Logic.Handlers
{
    public class GetUserSessionHandler : IRequestHandler<GetUserSession, Result<ControllerData>>
    {
        private readonly GameDbContext _context;

        private readonly IMapper _mapper;

        private readonly IValidator<GetUserSession> _validator;

        public GetUserSessionHandler(GameDbContext context, IMapper mapper, IValidator<GetUserSession> validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Result<ControllerData>> Handle(GetUserSession request, CancellationToken cancellationToken)
        {
            var validator = _validator.Validate(request);
            if (!validator.IsValid)
            {
                return Result.Fail<ControllerData>(validator.Errors.First().ErrorMessage);
            }
            var result = _mapper.Map<UserGameData, ControllerData>(_mapper.Map<GameDb, UserGameData>(await _context.Games.FindAsync(request._data.Id)));
            if (result != null)
            {
                return Result.Ok<ControllerData>(result);
            }
            else
            {
                return Result.Fail<ControllerData>("Session not found");
            }
        }
    }
}
