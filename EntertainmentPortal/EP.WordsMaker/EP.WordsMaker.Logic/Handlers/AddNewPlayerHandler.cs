using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.WordsMaker.Data.Context;
using EP.WordsMaker.Data.Models;
using EP.WordsMaker.Logic.Commands;
using EP.WordsMaker.Logic.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EP.WordsMaker.Logic.Handlers
{
    public class AddNewPlayerHandler : IRequestHandler<AddNewPlayerCommand, Result<Player>>
    {
        private readonly PlayerDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<AddNewPlayerCommand> _validator;

        public AddNewPlayerHandler(PlayerDbContext context, IMapper mapper, IValidator<AddNewPlayerCommand> validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Result<Player>> Handle(AddNewPlayerCommand request, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(request);

            if (!result.IsValid)
            {
                return Result.Fail<Player>(result.Errors.First().ErrorMessage);
            }

            var model = new PlayerDb
            {
                Name = request.Name,
				BestScore = 0
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