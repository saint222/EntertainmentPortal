using EP.DotsBoxes.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EP.DotsBoxes.Data
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPlayerData(this IServiceCollection services)
        {
            services.AddDbContext<PlayerDbContext>(
                opt => opt.UseSqlite("Data Source=player.db"));
           return services;
        }

        public static IServiceCollection CreateGameBoardData(this IServiceCollection services)
        {
           services.AddDbContext<GameBoardDbContext>(
                opt => opt.UseSqlite("Data Source=gameboard.db"));
            return services;
        }
    }
}
