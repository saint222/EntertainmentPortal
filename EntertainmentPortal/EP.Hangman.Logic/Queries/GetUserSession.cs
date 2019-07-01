using System;
using CSharpFunctionalExtensions;
using MediatR;
using EP.Hangman.Logic.Models;


namespace EP.Hangman.Logic.Queries
{
    public class GetUserSession : IRequest<Result<ControllerData>>
    {
        public ControllerData _data;

        public GetUserSession(string Id)
        {
            _data = new ControllerData {Id = Convert.ToInt64(Id)};
        }
    }
}
