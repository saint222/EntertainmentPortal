using CSharpFunctionalExtensions;
using EP.TicTacToe.Logic.Models;
using MediatR;

namespace EP.TicTacToe.Logic.Commands
{
    public class AddNewGameCommand : IRequest<Result<Game>>
    {
        public string PlayerOne { get; set; }
        public string PlayerTwo { get; set; }

        public int MapSize { get; set; }

        public int MapWinningChain { get; set; }
    }
}