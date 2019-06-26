using EP.TicTacToe.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EP.TicTacToe.Data
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPlayerData(this IServiceCollection services)
        {
            services.AddDbContext<PlayerDbContext>(
                opt =>
                {
                    opt.UseSqlite("Data Source=player.db");
                    opt.UseQueryTrackingBehavior(
                        QueryTrackingBehavior.NoTracking);
                });
            return services;
        }
    }
}
