using AutoMapper;
using EP.Sudoku.Data.Context;
using EP.Sudoku.Data.Models;
using EP.Sudoku.Logic.Commands;
using EP.Sudoku.Logic.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FluentValidation;

namespace EP.Sudoku.Logic.Handlers
{
    public class UpdatePlayerHandler : IRequestHandler<UpdatePlayerCommand, Result<Player>>
    {
        private readonly SudokuDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdatePlayerCommand> _validator;

        public UpdatePlayerHandler(SudokuDbContext context, IMapper mapper, IValidator<UpdatePlayerCommand> validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Result<Player>> Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(request, ruleSet: "CheckExistingEditPlayerValidation");

            if (result.Errors.Count > 0)
            {
                return Result.Fail<Player>(result.Errors.First().ErrorMessage);
            }

            var playerDb = _mapper.Map<PlayerDb>(request.player);
            playerDb.IconDb = _context.Find<AvatarIconDb>(request.player.Icon.Id);
            _context.Entry(playerDb).State = EntityState.Modified;            
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Ok<Player>(_mapper.Map<Player>(playerDb));
        }
    }
}
