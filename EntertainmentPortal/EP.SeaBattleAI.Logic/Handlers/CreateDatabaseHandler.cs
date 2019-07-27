using System.Threading;
using System.Threading.Tasks;
using EP.SeaBattle.Data.Context;
using EP.SeaBattle.Logic.Commands;
using MediatR;

namespace EP.SeaBattle.Logic.Handlers
{
    public class CreateDatabaseHandler : AsyncRequestHandler<CreateDatabaseCommand>
    {
        private readonly SeaBattleDbContext _context;

        public CreateDatabaseHandler(SeaBattleDbContext context)
        {
            _context = context;
        }

        protected override async Task Handle(CreateDatabaseCommand request, CancellationToken cancellationToken)
        {
            await _context.Database.EnsureCreatedAsync(cancellationToken);
        }
    }
}
