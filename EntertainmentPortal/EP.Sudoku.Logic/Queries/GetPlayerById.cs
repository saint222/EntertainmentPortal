using EP.Sudoku.Logic.Models;
using MediatR;

namespace EP.Sudoku.Logic.Queries
{
    public class GetPlayerById : IRequest<Player>
    {
        public int Id { get; set; }

        public GetPlayerById(int id)
        {
            Id = id;
        }
    }
}
