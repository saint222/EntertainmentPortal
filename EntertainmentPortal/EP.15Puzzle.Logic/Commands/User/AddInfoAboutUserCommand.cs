using System;
using System.Security.AccessControl;
using CSharpFunctionalExtensions;
using EP._15Puzzle.Logic.Models;
using MediatR;

namespace EP._15Puzzle.Logic.Commands
{
    public class AddInfoAboutUserCommand : IRequest <Result<User>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        public AddInfoAboutUserCommand(int id,string name, string country)
        {
            Id = id;
            Name = name;
            Country = country;
        }
    }
}
