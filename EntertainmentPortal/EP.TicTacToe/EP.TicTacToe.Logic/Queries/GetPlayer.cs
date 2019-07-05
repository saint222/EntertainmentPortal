using CSharpFunctionalExtensions;
using EP.TicTacToe.Logic.Models;
using MediatR;

namespace EP.TicTacToe.Logic.Queries
{
    public class GetPlayer : IRequest<Maybe<Player>>
    {
        public string Id { get; set; }
    }
}