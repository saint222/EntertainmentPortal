using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EP.Hangman.Data.Models;
using MediatR;
using EP.Hangman.Logic.Models;
using EP.Hangman.Logic.Queries;
using EP.Hangman.Data;

namespace EP.Hangman.Logic.Handlers
{
    public class GetHangmanHandler : IRequestHandler<GetHangman, UserGameData>
    {
        private  HangmanTemporaryData _item;
        private IMapper _mapper;
        public GetHangmanHandler(HangmanTemporaryData item, IMapper mapper)
        {
            _item = item;
            _mapper = mapper;
        }

        public Task<UserGameData> Handle(GetHangman request, CancellationToken cancellationToken)
        {
            var repository = new Repository();

            return Task.FromResult(_mapper.Map<HangmanTemporaryData, UserGameData>(repository.Select(_item)));
        }
    }
}
