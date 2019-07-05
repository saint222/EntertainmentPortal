using CSharpFunctionalExtensions;
using EP.TicTacToe.Logic.Models;
using MediatR;

namespace EP.TicTacToe.Logic.Queries
{
    public class GetGame : IRequest<Maybe<Game>>
    {
        public int Id { get; set; }
    }
}