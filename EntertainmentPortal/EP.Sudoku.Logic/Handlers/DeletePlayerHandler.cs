using AutoMapper;
using EP.Sudoku.Data;
using EP.Sudoku.Data.Models;
using EP.Sudoku.Logic.Models;
using EP.Sudoku.Logic.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace EP.Sudoku.Logic.Handlers
{
    public class DeletePlayerHandler : IRequestHandler<DeletePlayer, bool>
    {
        public Task<bool> Handle(DeletePlayer request, CancellationToken cancellationToken)
        {
            var deletedPlayer = PlayerStorage.Players.Where(x => x.Id == request.Id).FirstOrDefault();
            if (deletedPlayer == null)
            {
                return Task.FromResult(false);
            }
            PlayerStorage.Players.Remove(deletedPlayer);
            return Task.FromResult(true);
        }
    }
}
