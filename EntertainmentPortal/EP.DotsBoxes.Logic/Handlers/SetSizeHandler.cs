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
        private GameBoardData _boardData;

        public SetSizeHandler(GameBoardData gameBoard)
        {
            _boardData = gameBoard;
        }

        public Task<int[,]> Handle(SetSize request, CancellationToken cancellationToken)
        {

            var gameBoard = new GameBoard()
            {
                Row = request.NewRow,
                Column = request.NewColumn
            };

            var array = _boardData.Save(new int[gameBoard.Row,gameBoard.Column]);
            return Task.FromResult(array);
        }

    }
}
