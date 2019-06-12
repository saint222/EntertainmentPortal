using CSharpFunctionalExtensions;
using EP.Balda.Logic.Models;
using MediatR;

namespace EP.Balda.Logic.Queries
{
    public class GetCell : IRequest<Maybe<Cell>>
    {
        public int X { get; set; }

        public int Y { get; set; }

        public GetCell(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}