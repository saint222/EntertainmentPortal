using System.Linq;
using MediatR;
using EP.Hangman.Data.Models;
using EP.Hangman.Logic.Models;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EP.Hangman.Data.Context;
using EP.Hangman.Logic.Commands;
using Microsoft.EntityFrameworkCore;


namespace EP.Hangman.Logic.Handlers
{
    public class CheckLetterHandler : IRequestHandler<CheckLetterCommand, ControllerData>
    {
        private readonly GameDbContext _context;

        private readonly IMapper _mapper;

        public CheckLetterHandler(GameDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ControllerData> Handle(CheckLetterCommand request, CancellationToken cancellationToken)
        {
            var session = await _context.Games.FindAsync(request._data.Id);

            var result = new HangmanGame(_mapper.Map<GameDb, UserGameData>(session)).Play(request._data.Letter);

            var mapped = _mapper.Map<UserGameData, GameDb>(result);

            _context.Entry<GameDb>(mapped).State = EntityState.Modified;

            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            
            return _mapper.Map<UserGameData, ControllerData>(result);
        }
    }
}
