using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.DotsBoxes.Data.Context;
using EP.DotsBoxes.Data.Models;
using EP.DotsBoxes.Logic.Commands;
using EP.DotsBoxes.Logic.Models;
using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EP.DotsBoxes.Logic.Handlers
{
    public class AddPLayerHandler : IRequestHandler<AddPlayerCommand, Result<Player>>
    {
        private readonly PlayerDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<AddPlayerCommand> _validator;

        public AddPLayerHandler(PlayerDbContext context, IMapper mapper, IValidator<AddPlayerCommand> validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Result<Player>> Handle([NotNull]AddPlayerCommand request, CancellationToken cancellationToken)
        {
            //validate
            var result = _validator.Validate(request);

            if (result == null)
            {
                return Result.Fail<Player>(result.Errors.First().ErrorMessage);
            }

            var model = new PlayerDb
            {
                Name = request.Name,
                Color = request.Color
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
