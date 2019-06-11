using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Balda.Data.Context;
using EP.Balda.Data.Models;
using EP.Balda.Logic.Commands;
using EP.Balda.Logic.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EP.Balda.Logic.Handlers
{
    public class CreateNewPlayerHandler : IRequestHandler<AddNewPlayerCommand, Result<Player>>
    {
        private readonly PlayerDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator _validator;

        public CreateNewPlayerHandler(PlayerDbContext context, IMapper mapper, IValidator validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Result<Player>> Handle(AddNewPlayerCommand request, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(request);

            if (result != null)
            {
                return Result.Fail<Player>(result.Errors.First().ErrorMessage);
            }

            var model = new PlayerDb
            {
                NickName = request.NickName,
                Login = request.Login,
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
