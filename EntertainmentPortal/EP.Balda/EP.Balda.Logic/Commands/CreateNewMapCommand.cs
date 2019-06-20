using CSharpFunctionalExtensions;
using EP.Balda.Logic.Models;
using MediatR;

namespace EP.Balda.Logic.Commands
{
    public class CreateNewMapCommand : IRequest<Result<Map>>
    {
    }
}