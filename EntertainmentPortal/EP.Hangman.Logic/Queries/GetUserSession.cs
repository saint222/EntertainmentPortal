using System;
using MediatR;
using EP.Hangman.Logic.Models;


namespace EP.Hangman.Logic.Queries
{
    public class GetUserSession : IRequest<ControllerData>
    {
        public ControllerData _data;

        public GetUserSession(ControllerData data, string id)
        {
            _data = data;
            _data.Id = Convert.ToInt64(id);
        }
    }
}
