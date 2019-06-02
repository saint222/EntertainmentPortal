using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EP.DotsBoxes.Data.Context;
using EP.DotsBoxes.Logic.Commands;
using MediatR;

namespace EP.DotsBoxes.Logic.Handlers
{
    public class CreateDataBaseHandler : AsyncRequestHandler<CreateDatabaseCommand>
    {
        private readonly PlayerDbContext _playerContext;
        private readonly GameBoardDbContext _gameBoardContext;

        public CreateDataBaseHandler(PlayerDbContext playerContext, GameBoardDbContext gameBoardContext)
        {
            _playerContext = playerContext;
            _gameBoardContext = gameBoardContext;
        }

        protected override async Task Handle(CreateDatabaseCommand request, CancellationToken cancellationToken)
        {
            await _gameBoardContext.Database.EnsureCreatedAsync(cancellationToken);
            await _playerContext.Database.EnsureCreatedAsync(cancellationToken);
        }
    }
}
