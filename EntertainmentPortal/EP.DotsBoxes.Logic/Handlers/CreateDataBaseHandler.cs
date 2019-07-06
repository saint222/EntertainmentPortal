using System.Threading;
using System.Threading.Tasks;
using EP.DotsBoxes.Data.Context;
using EP.DotsBoxes.Logic.Commands;
using MediatR;

namespace EP.DotsBoxes.Logic.Handlers
{
    public class CreateDataBaseHandler : AsyncRequestHandler<CreateDatabaseCommand>
    {
        private readonly GameBoardDbContext _gameBoardContext;

        public CreateDataBaseHandler(GameBoardDbContext gameBoardContext)
        {
            _gameBoardContext = gameBoardContext;
        }

        protected override async Task Handle(CreateDatabaseCommand request, CancellationToken cancellationToken)
        {
            await _gameBoardContext.Database.EnsureCreatedAsync(cancellationToken);
        }
    }
}
