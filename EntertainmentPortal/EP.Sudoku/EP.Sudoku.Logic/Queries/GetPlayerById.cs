using EP.Sudoku.Logic.Models;
using MediatR;

namespace EP.Sudoku.Logic.Queries
{
    public class GetPlayerById : IRequest<Player>
    {
        public long Id { get; set; }

        public GetPlayerById(long id)
        {
            Id = id;
        }
    }
}
