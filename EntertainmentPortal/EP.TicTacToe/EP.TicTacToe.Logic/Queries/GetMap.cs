using CSharpFunctionalExtensions;
using EP.TicTacToe.Logic.Models;
using MediatR;

namespace EP.TicTacToe.Logic.Queries
{
    public class GetMap : IRequest<Maybe<Map>>
    { 
        public uint Id { get; set; }
    }
}