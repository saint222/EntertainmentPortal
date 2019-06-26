using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EP.TicTacToe.Data.Models;
using EP.TicTacToe.Data.Context;
using EP.TicTacToe.Logic.Models;
using EP.TicTacToe.Logic.Commands;
using CSharpFunctionalExtensions;
using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EP.TicTacToe.Logic.Handlers
{
    public class AddNewPlayerHandler : IRequestHandler<AddNewPlayerCommand, Result<Player>>
    {
        private readonly PlayerDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<AddNewPlayerCommand>[] _validators;

        public AddNewPlayerHandler(PlayerDbContext context, IMapper mapper, IValidator<AddNewPlayerCommand>[] validators)
        {
            _context = context;
            _mapper = mapper;
            _validators = validators;
        }

        public async Task<Result<Player>> Handle([NotNull]AddNewPlayerCommand request, CancellationToken cancellationToken)
        {
            //validate
            var result = _validators.Select(x => x.Validate(request)).FirstOrDefault(x => !x.IsValid);
            if (result != null)
            {
                return Result.Fail<Player>(result.Errors.First().ErrorMessage);
            }

            var model = new PlayerDb
            {
                NickName = request.NickName,
                Password = request.Password
            };

            _context.Players.Add(model);

            try
            {
                await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                return Result.Ok<Player>(_mapper.Map<Player>(model));
            }
            catch (DbUpdateException ex)
            {
                return Result.Fail<Player>(ex.Message);
            }            
        }
    }
}
