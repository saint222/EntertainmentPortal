using Bogus;

namespace EP.Balda.Data.Models.ModelBuilder
{
    internal static class ModelBuilderExtensions
    {
        private const int CONT_INIT_PLAYERS = 10;
        private static readonly Faker<PlayerDb> _faker = new Faker<PlayerDb>();

        public static void Seed(
            this Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
        {
            var faker =
                _faker.RuleFor(x => x.Id, f => ++f.IndexFaker)
                    .RuleFor(x => x.Score, f => f.Random.Int(10, 100))
                    .RuleFor(x => x.NickName, f => f.Person.UserName)
                    .RuleFor(x => x.Login, f => f.Internet.UserName())
                    .RuleFor(x => x.Password, f => f.Internet.Password(5));

            var playersGenerate = faker.Generate(CONT_INIT_PLAYERS);
            foreach (var player in playersGenerate)
                //Console.WriteLine($"registered playerId = {player.Id}, {player.Login}");

                modelBuilder.Entity<PlayerDb>().HasData(
                    new PlayerDb
                    {
                        Id = player.Id,
                        Login = player.Login,
                        NickName = player.NickName,
                        Password = player.Password,
                        Score = player.Score
                    });
        }
    }
}