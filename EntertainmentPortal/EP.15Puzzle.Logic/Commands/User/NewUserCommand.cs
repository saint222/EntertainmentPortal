using System;
using System.Security.AccessControl;
using CSharpFunctionalExtensions;
using EP._15Puzzle.Logic.Models;
using MediatR;

namespace EP._15Puzzle.Logic.Commands
{
    public class NewUserCommand : IRequest <Result<User>>
    {
        public string Name { get; set; }
        public string Country { get; set; }

        public NewUserCommand(string name, string country)
        {
            Name = name;
            Country = country;
        }
    }
}
