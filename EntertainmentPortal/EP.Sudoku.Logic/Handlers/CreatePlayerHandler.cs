using System.Linq;
using AutoMapper;
using EP.Sudoku.Data.Context;
using EP.Sudoku.Data.Models;
using EP.Sudoku.Logic.Commands;
using EP.Sudoku.Logic.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FluentValidation;

namespace EP.Sudoku.Logic.Handlers
{
    public class CreatePlayerHandler : IRequestHandler<CreatePlayerCommand, Result<Player>>
    {
        private readonly SudokuDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<CreatePlayerCommand> _validator;

        public CreatePlayerHandler(SudokuDbContext context, IMapper mapper, IValidator<CreatePlayerCommand> validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<Result<Player>> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(request, ruleSet: "CheckExistingPlayerValidation");

            if (result.Errors.Count > 0)
            {
                return Result.Fail<Player>(result.Errors.First().ErrorMessage);
            }

            var playerDb = new PlayerDb()
            {
                NickName = request.NickName
            };
            playerDb.IconDb = _context.Find<AvatarIconDb>(request.IconId);
            playerDb.GameSessionDb = null;
            _context.Add(playerDb);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Ok<Player>(_mapper.Map<Player>(playerDb));
        }
    }
}
