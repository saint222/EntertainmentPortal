using CSharpFunctionalExtensions;
using EP.TicTacToe.Logic.Models;
using MediatR;

namespace EP.TicTacToe.Logic.Queries
{
    public class GetCell : IRequest<Maybe<Cell>>
    {
        public int Id { get; set; }
    }
}