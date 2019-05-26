using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Sudoku.Logic.Handlers
{
    public class PostPlayerHandler : IRequestHandler<PostPlayer, PlayerDb>
    {
        private PlayerDb _item;
        public PostPlayerHandler(PlayerDb item)
        {
            _item = item;
        }

        public Task<PlayerDb> Handle(PostPlayer request, CancellationToken cancellationToken)
        {
            var repository = new Repository();

            return Task.FromResult(repository.Create(_item));
        }

    }
}
