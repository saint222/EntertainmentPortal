using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EP.Hagman.Data.Models;
using MediatR;
using EP.Hangman.Logic.Models;
using EP.Hangman.Logic.Queries;
using EP.Hagman.Data;

namespace EP.Hangman.Logic.Handlers
{
    public class PostHangmanHandler : IRequestHandler<PostHangman, HangmanDataResponse>
    {
        private HangmanTemporaryData _item;
        private IMapper _mapper;
        public PostHangmanHandler(HangmanTemporaryData item, IMapper mapper)
        {
            _item = item;
            _mapper = mapper;
        }

        public Task<HangmanDataResponse> Handle(PostHangman request, CancellationToken cancellationToken)
        {
            var repository = new Repository();

            return Task.FromResult(_mapper.Map<HangmanTemporaryData, HangmanDataResponse>(repository.Create(_item)));
        }
    }
}
