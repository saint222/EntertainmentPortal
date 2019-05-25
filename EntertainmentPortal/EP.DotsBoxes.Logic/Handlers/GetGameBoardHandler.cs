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
        private GameBoardData _boardData;

        public GetGameBoardHandler(GameBoardData gameBoard)
        {
            _boardData = gameBoard;
        }

        public Task<int[,]> Handle(GetGameBoard request, CancellationToken cancellationToken)
        {
            var gameBoardArray = _boardData.GetGameBoard;
               

            return Task.FromResult((int[,])gameBoardArray);
        }

    }
}
