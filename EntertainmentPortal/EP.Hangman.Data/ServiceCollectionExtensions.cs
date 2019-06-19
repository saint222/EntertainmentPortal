using EP.Hangman.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EP.Hangman.Data
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGameData(this IServiceCollection services)
        {
            services.AddDbContext<GameDbContext>(opt => opt.UseSqlite("Data Source = game.db"));
            return services;
        }
    }
}
