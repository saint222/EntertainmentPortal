using CSharpFunctionalExtensions;
using MediatR;

namespace EP._15Puzzle.Logic.Commands
{
    public class ResetDeckCommand : IRequest<Result<Deck>>
    {
        public int Id { get; }
        public ResetDeckCommand(int id)
        {
            Id = id;
        }
    }
}
