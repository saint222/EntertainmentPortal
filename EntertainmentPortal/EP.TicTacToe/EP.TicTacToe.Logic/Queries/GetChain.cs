using CSharpFunctionalExtensions;
using EP.TicTacToe.Logic.Models;
using MediatR;

namespace EP.TicTacToe.Logic.Queries
{
    public class GetChain : IRequest<Maybe<Chain>>
    { 
        public uint Id { get; set; }
    }
}