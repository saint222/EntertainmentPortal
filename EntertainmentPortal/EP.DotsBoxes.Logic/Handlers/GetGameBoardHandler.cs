using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using EP.DotsBoxes.Data;
using EP.DotsBoxes.Logic.Models;
using EP.DotsBoxes.Logic.Queries;
using MediatR;

namespace EP.DotsBoxes.Logic.Handlers
{
    public class GetGameBoardHandler : IRequestHandler<GetGameBoard, int[,]>
    {
        private GameBoardData _item;

        public GetGameBoardHandler(GameBoardData gameBoard)
        {
            _item = gameBoard;
        }

        public Task<int[,]> Handle(GetGameBoard request, CancellationToken cancellationToken)
        {
            var result = _item.Get;
              
            return Task.FromResult(result);
        }

    }
}
