using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EP.Hangman.Data.Context;
using EP.Hangman.Data.Models;
using EP.Hangman.Logic.Commands;
using EP.Hangman.Logic.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EP.Hangman.Logic.Handlers
{
    public class DeleteGameSessionCommandHandler : IRequestHandler<DeleteGameSessionCommand, ControllerData>
    {
        private readonly GameDbContext _context;

        private readonly IMapper _mapper;

        public DeleteGameSessionCommandHandler(GameDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ControllerData> Handle(DeleteGameSessionCommand request, CancellationToken cancellationToken)
        {
            var result = new GameDb(){Id = request._data.Id};

            _context.Entry<GameDb>(result).State = EntityState.Deleted;

            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            
            return null;
        }
    }
}
