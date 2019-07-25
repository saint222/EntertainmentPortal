using CSharpFunctionalExtensions;
using EP.Sudoku.Logic.Models;
using MediatR;

namespace EP.Sudoku.Logic.Queries
{
    public class GetPlayerByUserId : IRequest<Maybe<Player>>
    {
        public string Id { get; set; }

        public GetPlayerByUserId(string id)
        {
            Id = id;
        }
    }
}
