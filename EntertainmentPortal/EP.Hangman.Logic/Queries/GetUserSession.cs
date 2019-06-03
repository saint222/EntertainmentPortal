using System;
using MediatR;
using EP.Hangman.Logic.Models;


namespace EP.Hangman.Logic.Queries
{
    public class GetUserSession : IRequest<UserGameData>
    {
        public GetUserSession(string id)
        {
            Id = Convert.ToInt64(id);
        }

        public long  Id { get; set; }
    }
}
