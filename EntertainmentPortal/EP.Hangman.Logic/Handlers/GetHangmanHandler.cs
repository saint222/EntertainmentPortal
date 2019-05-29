using System;
using System.Collections.Generic;
using System.Dynamic;
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
using EP.Hagman.Logic.Interfaces;

namespace EP.Hangman.Logic.Handlers
{
    public class GetHangmanHandler : IRequestHandler<GetHangman, HangmanTemporaryData>
    {
        private  HangmanTemporaryData _item;
        private IMapper _mapper;
        public GetHangmanHandler(HangmanTemporaryData item, IMapper mapper)
        {
            _item = item;
            _mapper = mapper;
        }

        public Task<HangmanTemporaryData> Handle(GetHangman request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_item);
        }
    }
}
