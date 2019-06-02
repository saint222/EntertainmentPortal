using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using EP.Hangman.Data.Models;
using EP.Hangman.Logic.Models;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EP.Hangman.Data.Context;
using EP.Hangman.Logic.Commands;


namespace EP.Hangman.Logic.Handlers
{
    public class CheckLetterHandler : IRequestHandler<CheckLetterCommand, UserGameData>
    {
        private GameDbContext _context;
        private IMapper _mapper;
        public CheckLetterHandler(GameDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<UserGameData> Handle(CheckLetterCommand request, CancellationToken cancellationToken)
        {
            var session = await _context.Games.FindAsync(request.Id);
            var result = new HangmanGame(session).Play(request.Letter);
            _context.Games.Update(result);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return _mapper.Map<GameDb, UserGameData>(result);
        }
    }
}
