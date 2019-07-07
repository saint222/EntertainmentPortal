using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Balda.Data.Context;
using EP.Balda.Data.Models;
using EP.Balda.Logic.Commands;
using EP.Balda.Logic.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EP.Balda.Logic.Handlers
{
    public class
        CreateNewPlayerHandler : IRequestHandler<CreateNewPlayerCommand, Result<Player>>
    {
        private readonly BaldaGameDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateNewPlayerCommand> _validator;


        public CreateNewPlayerHandler(BaldaGameDbContext context, IMapper mapper,
                                      IValidator<CreateNewPlayerCommand> validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Result<Player>> Handle(CreateNewPlayerCommand request,
                                                 CancellationToken cancellationToken)
        {
            var result = await _validator
                .ValidateAsync(request, ruleSet: "PlayerCreateExistingSet", cancellationToken: cancellationToken);

            if (!result.IsValid)
            {
                return Result.Fail<Player>(result.Errors.First().ErrorMessage);
            }

            var playerDb = new PlayerDb
            {
                NickName = request.NickName,
                Login = request.Login,
                Password = request.Password,
                Created = DateTime.UtcNow
            };

            _context.Players.Add(playerDb);

            try
            {
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Ok(_mapper.Map<Player>(playerDb));
            }
            catch (DbUpdateException ex)
            {
                return Result.Fail<Player>(ex.Message);
            }
        }
    }
}