using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using EP.Hangman.Logic.Queries;
using EP.Hangman.Data;
using EP.Hangman.Logic.Models;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;


namespace EP.Hangman.Logic.Handlers
{
    public class PutHangmanHandler : IRequestHandler<PutHangman, UserGameData>
    {
        private HangmanTemporaryData _item;
        private IMapper _mapper;
        public PutHangmanHandler(HangmanTemporaryData item, IMapper mapper)
        {
            _item = item;
            _mapper = mapper;
        }
        public Task<UserGameData> Handle(PutHangman request, CancellationToken cancellationToken)
        {
            var repository = new Repository();
            return Task.FromResult(_mapper.Map<HangmanTemporaryData, UserGameData>(repository.Update(_item, request.LetterToCheck.Substring(0, 1).ToUpper())));

        }
    }
}
