using EP.WordsMaker.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EP.WordsMaker.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPlayerData(this IServiceCollection services)
        {
            services.AddDbContext<PlayerDbContext>(opt => opt.UseSqlite("Data Source=player.db"));
            return services;
        }
    }
}