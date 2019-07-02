using System.Collections.Generic;
using CSharpFunctionalExtensions;
using EP.TicTacToe.Logic.Models;
using MediatR;

namespace EP.TicTacToe.Logic.Queries
{
    public class GetAllPlayers : IRequest<Maybe<IEnumerable<Player>>>
    {
    }
}