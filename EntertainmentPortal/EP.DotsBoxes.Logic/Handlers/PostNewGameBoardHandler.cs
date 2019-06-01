using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EP.DotsBoxes.Data;
using EP.DotsBoxes.Data.Models;
using EP.DotsBoxes.Logic.Models;
using EP.DotsBoxes.Logic.Queries;
using MediatR;

namespace EP.DotsBoxes.Logic.Handlers
{
    public class PostNewGameBoardHandler : IRequestHandler<PostNewGameBoard, int[,]>
    {
        private GameBoardData _item;
        private readonly IMapper _mapper;

        public PostNewGameBoardHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public PostNewGameBoardHandler(GameBoardData gameBoard)
        {
            _item = gameBoard;
        }

        public Task<int[,]> Handle(PostNewGameBoard request, CancellationToken cancellationToken)
        {
            var gameBoard = new GameBoard()
            {
                Row = request.Rows,
                Column = request.Columns
            };

            var gameBoardDb = _mapper.Map<GameBoardDb>(gameBoard);
           
            return Task.FromResult(_item.Create(gameBoardDb));
        }
    }
}
