using EP.Sudoku.Logic.Models;
using MediatR;
using CSharpFunctionalExtensions;

namespace EP.Sudoku.Logic.Commands
{
    public class CreatePlayerCommand : IRequest<Result<Player>>
    {
        public string NickName { get; set; }

        public long IconId { get; set; }

        public string UserId { get; set; }
    }
}
