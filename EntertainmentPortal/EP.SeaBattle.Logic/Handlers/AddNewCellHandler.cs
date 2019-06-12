using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EP.SeaBattle.Data.Context;
using EP.SeaBattle.Data.Models;
using EP.SeaBattle.Logic.Commands;
using EP.SeaBattle.Logic.Models;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EP.SeaBattle.Logic.Handlers
{
    public class AddNewCellHandler : IRequestHandler<AddNewCellCommand, Result<Cell>>
    {

        private readonly SeaBattleDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<AddNewCellCommand> _validator;


        public AddNewCellHandler(SeaBattleDbContext context, IMapper mapper, IValidator<AddNewCellCommand> validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }


        public async Task<Result<Cell>> Handle(AddNewCellCommand request, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(request);
            if (result.IsValid)
            {
                var cell = _mapper.Map<Cell>(request);
                _context.Cells.Add(_mapper.Map<CellDb>(request));
                try
                {
                    await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                    return Result.Ok<Cell>(cell);
                }
                catch (DbUpdateException ex)
                {

                    return Result.Fail<Cell>(ex.Message);
                }
            }
            else
                return Result.Fail<Cell>(string.Join("; ", result.Errors));
        }
    }
}
