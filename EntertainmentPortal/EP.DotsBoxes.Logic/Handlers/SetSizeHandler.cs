using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EP.DotsBoxes.Data;
using EP.DotsBoxes.Logic.Models;
using EP.DotsBoxes.Logic.Queries;
using MediatR;

namespace EP.DotsBoxes.Logic.Handlers
{
    public class SetSizeHandler : IRequestHandler<SetSize, int[,]>
    {
        private GameBoardData _gameBoardData;

        public SetSizeHandler(GameBoardData gameGameBoard)
        {
            _gameBoardData = gameGameBoard;
        }

        public Task<int[,]> Handle(SetSize request, CancellationToken cancellationToken)
        {
            var gameBoard = new GameBoard()
            {
                Row = request.Rows,
                Column = request.Columns
            };

            _gameBoardData.Save(new int[gameBoard.Row,gameBoard.Column]);
            return Task.FromResult(_gameBoardData.GetGameBoard);
        }

    }
}
