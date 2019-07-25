using CSharpFunctionalExtensions;
using EP.Sudoku.Logic.Models;
using MediatR;

namespace EP.Sudoku.Logic.Commands
{
    public class GetHintCommand : IRequest<Result<Session>>
    {
        public int Id { get; set; }
        
        public int SessionId { get; set; }
    }
}
