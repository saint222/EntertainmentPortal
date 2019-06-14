using System.Collections.Generic;
using EP.TicTacToe.Data.Models;
using CSharpFunctionalExtensions;
using MediatR;

namespace EP.TicTacToe.Logic.Queries
{
    public class GetAllPlayers: IRequest<Maybe<IEnumerable<PlayerDb>>>
    {
    }
}
