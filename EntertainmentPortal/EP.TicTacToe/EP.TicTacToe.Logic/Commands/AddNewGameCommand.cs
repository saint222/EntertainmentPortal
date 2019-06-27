using EP.TicTacToe.Logic.Models;
using MediatR;

namespace EP.TicTacToe.Logic.Commands
{
    public class AddNewGameCommand : IRequest<Game>
    {
    }
}