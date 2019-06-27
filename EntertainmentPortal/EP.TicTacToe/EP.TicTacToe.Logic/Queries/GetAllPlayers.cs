using System.Collections.Generic;
using CSharpFunctionalExtensions;
using EP.TicTacToe.Data.Models;
using MediatR;

namespace EP.TicTacToe.Logic.Queries
{
    public class GetAllPlayers : IRequest<Maybe<IEnumerable<PlayerDb>>>
    {
    }
}