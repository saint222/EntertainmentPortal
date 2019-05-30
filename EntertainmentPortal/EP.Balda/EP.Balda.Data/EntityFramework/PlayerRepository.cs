using System.Collections.Generic;
using Bogus;
using EP.Balda.Data.Entity;

namespace EP.Balda.Data.EntityFramework
{
    public static class PlayerRepository
    {
        private static readonly Faker<PlayerDb> _faker = new Faker<PlayerDb>();

        public static List<PlayerDb> Players => _faker.Generate(5);

        static PlayerRepository()
        {
            _faker.RuleFor(x => x.NickName, f => f.Person.UserName)
                .RuleFor(x => x.FirstName, f => f.Name.FirstName())
                .RuleFor(x => x.LastName, f => f.Name.LastName())
                .RuleFor(x => x.FirstName, f => f.Name.FirstName())
                .RuleFor(x => x.Login, f => f.Person.Email.ToString())
                .RuleFor(x => x.Password, f => f.Lorem.Word())
                .RuleFor(x => x.Result, f => f.Random.Int(5, 25));
        }
    }
}