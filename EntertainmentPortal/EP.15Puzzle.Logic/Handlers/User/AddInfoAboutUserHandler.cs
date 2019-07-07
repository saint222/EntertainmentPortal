using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP._15Puzzle.Data;
using EP._15Puzzle.Data.Context;
using EP._15Puzzle.Data.Models;
using EP._15Puzzle.Logic.Commands;
using EP._15Puzzle.Logic.Models;
using EP._15Puzzle.Logic.Queries;
using EP._15Puzzle.Logic.Validators;
using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EP._15Puzzle.Logic.Handlers
{
    public class AddInfoAboutUserHandler : IRequestHandler<AddInfoAboutUserCommand, Result<User>>
    {
        private readonly IMapper _mapper;
        //private readonly IValidator<NewUserCommand> _validator;
        private readonly DeckDbContext _context;
        

        public AddInfoAboutUserHandler(DeckDbContext context, IMapper mapper)//, IValidator<NewUserCommand> validator)
        {
            _mapper = mapper;
            //_validator = validator;
            _context = context;
        }
        public async Task<Result<User>> Handle(AddInfoAboutUserCommand request, CancellationToken cancellationToken)
        {
            //validate
            /*var result = await _validator.ValidateAsync(request,ruleSet: "IdExistingSet",cancellationToken: cancellationToken)
                .ConfigureAwait(false);
            if (!result.IsValid)
            {
                return Result.Fail<User>(result.Errors.First().ErrorMessage);
            }*/


            try
            {
                var userDb = _context.UserDbs.First(u => u.Id == request.Id);
                userDb.Country = request.Country;
                userDb.Name = request.Name;
                _context.Update(userDb);
                await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                return await Task.FromResult(Result.Ok<User>(_mapper.Map<User>(userDb)));
            }
            catch (DbException ex)
            {
                return Result.Fail<User>(ex.Message);
            }
        }
    }
}
