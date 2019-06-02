using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EP.DotsBoxes.Data;
using EP.DotsBoxes.Data.Context;
using EP.DotsBoxes.Data.Models;
using EP.DotsBoxes.Logic.Commands;
using EP.DotsBoxes.Logic.Models;
using EP.DotsBoxes.Logic.Queries;
using MediatR;

namespace EP.DotsBoxes.Logic.Handlers
{
    public class CreateNewGameBoardHandler : IRequestHandler<CreateNewGameBoardCommand, GameBoard>
    {
        private readonly IMapper _mapper;
        private readonly GameBoardDbContext _context;

        public CreateNewGameBoardHandler(IMapper mapper, GameBoardDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<GameBoard> Handle(CreateNewGameBoardCommand request, CancellationToken cancellationToken)
        {
            var model = new GameBoardDb()
            {
                Rows = request.Rows,
                Columns = request.Columns
            };

            _context.GameBoard.Add(model);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return _mapper.Map<GameBoard>(model);
        }
    }
}
