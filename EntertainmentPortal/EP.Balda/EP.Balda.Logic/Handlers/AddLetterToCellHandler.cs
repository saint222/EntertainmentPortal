﻿using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Balda.Data.Context;
using EP.Balda.Logic.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using EP.Balda.Logic.Models;
using EP.Balda.Data.Models;
using FluentValidation;

namespace EP.Balda.Logic.Handlers
{
    public class
        AddLetterToCellHandler : IRequestHandler<AddLetterToCellCommand, Result<Cell>>
    {
        private readonly IMapper _mapper;
        private readonly BaldaGameDbContext _context;
        private readonly IValidator<AddLetterToCellCommand> _validator;

        public AddLetterToCellHandler(IMapper mapper, BaldaGameDbContext context, IValidator<AddLetterToCellCommand> validator)
        {
            _mapper = mapper;
            _context = context;
            _validator = validator;
        }

        public async Task<Result<Cell>> Handle(AddLetterToCellCommand request,
                                               CancellationToken cancellationToken)
        {
            var result = await _validator
                .ValidateAsync(request, ruleSet: "CellExistingSet", cancellationToken: cancellationToken);

            if (!result.IsValid)
            {
                return Result.Fail<Cell>(result.Errors.First().ErrorMessage);
            }

            var cellDb = await (_context.Cells
                .Where(c => c.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken));

            var isAllowedCell = await IsAllowedCell(cellDb);

            if(!isAllowedCell)
            {
                return Result.Fail<Cell>($"The cell with id {request.Id} doesn't have occupied cells nearby");
            }

            if(cellDb.Letter == null)
            {
                cellDb.Letter = request.Letter;
            }
            else
            {
                return Result.Fail<Cell>("Cell already contains letter");
            }

            try
            {
                await _context.SaveChangesAsync(cancellationToken);

                cellDb.Map = null; //remove lately

                return Result.Ok(_mapper.Map<Cell>(cellDb));
            }
            catch (DbUpdateException ex)
            {
                return Result.Fail<Cell>(ex.Message);
            }
        }

        /// <summary>
        ///     The method checks if the cell is allowed to insert a new letter.
        /// </summary>
        /// <param name="x">Parameter x requires an integer argument.</param>
        /// <param name="y">Parameter y requires an integer argument.</param>
        /// <param name="cellDb"></param>
        /// <returns>returns true if allowed</returns>
        public async Task<bool> IsAllowedCell(CellDb cellDb)
        {
            var map = await _context.Maps.Include(m => m.Cells)
                .Where(m => m.Id == cellDb.MapId).FirstOrDefaultAsync();

            var cellTop =
                map.Cells.FirstOrDefault(c =>
                    c.X == cellDb.X & c.Y == cellDb.Y + 1); // cell on top
            var cellDown =
                map.Cells.FirstOrDefault(c =>
                    c.X == cellDb.X & c.Y == cellDb.Y - 1); // bottom cell
            var cellRight =
                map.Cells.FirstOrDefault(c =>
                    c.X == cellDb.X + 1 & c.Y == cellDb.Y); // right cell
            var cellLeft =
                map.Cells.FirstOrDefault(c =>
                    c.X == cellDb.X - 1 & c.Y == cellDb.Y); // left cell

            if (cellTop?.Letter != null)
                return true;

            if (cellDown?.Letter != null)
                return true;

            if (cellLeft?.Letter != null)
                return true;

            if (cellRight?.Letter != null)
                return true;

            return false;
        }
    }
}