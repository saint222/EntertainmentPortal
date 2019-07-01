using System.Threading;
using System.Threading.Tasks;
using EP.Hangman.Data.Context;
using EP.Hangman.Logic.Commands;
using MediatR;

[assembly:Fody.ConfigureAwait(false)]
namespace EP.Hangman.Logic.Handlers
{
    public class CreateDatabaseHandler : AsyncRequestHandler<CreateDatabaseCommand>
    {
        private readonly GameDbContext _context;

        public CreateDatabaseHandler(GameDbContext context)
        {
            _context = context;
        }

        protected override async Task Handle(CreateDatabaseCommand request, CancellationToken cancellationToken)
        {
            await _context.Database.EnsureCreatedAsync(cancellationToken);
        }
    }
}
