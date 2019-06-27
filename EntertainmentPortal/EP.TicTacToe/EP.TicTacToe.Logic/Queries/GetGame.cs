using EP.TicTacToe.Logic.Models;
using MediatR;

namespace EP.TicTacToe.Logic.Queries
{
    public class GetGame : IRequest<Game>
    {
        public uint Id { get; set; }
    }
}