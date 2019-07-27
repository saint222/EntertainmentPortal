using EP.SeaBattle.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EP.SeaBattle.Data
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSeaBattleData(this IServiceCollection services)
        {
            services.AddDbContext<SeaBattleDbContext>(
                opt =>
                {
                    opt.UseSqlite("Data Source=seabattle.db");
                });
            return services;
        }
    }
}
