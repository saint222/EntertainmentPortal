using EP.Balda.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EP.Balda.Data.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection WordData(this IServiceCollection services)
        {
            services.AddDbContext<WordDbContext>(
                opt =>
                {
                    opt.UseSqlite(
                        "Data Source=" +
                        @"DbStore\dictionaryDb.db");
                    opt.UseQueryTrackingBehavior(
                        QueryTrackingBehavior.NoTracking);
                });
            return services;
        }

        public static IServiceCollection PlayerData(this IServiceCollection services)
        {
            services.AddDbContext<PlayerDbContext>(
                opt =>
                {
                    opt.UseSqlite(
                        "Data Source=" +
                        @"DbStore\plaerDb.db",x => x.MigrationsAssembly("EP.Balda.Data"));
                    opt.UseQueryTrackingBehavior(
                        QueryTrackingBehavior.NoTracking);
        });
            return services;
        }
    }
}