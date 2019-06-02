using System;
using System.Collections.Generic;
using System.Text;
using EP.Hangman.Data;
using MediatR;
using EP.Hangman.Logic.Models;


namespace EP.Hangman.Logic.Queries
{
    public class GetUserSession : IRequest<UserGameData>
    {
        public GetUserSession(string ID)
        {
            Id = Convert.ToInt64(ID);
        }

        public Int64  Id { get; set; }
    }
}
