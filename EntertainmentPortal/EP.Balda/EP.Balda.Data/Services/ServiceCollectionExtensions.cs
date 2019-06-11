using EP.Balda.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EP.Balda.Data.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWordData(this IServiceCollection services)
        {
            const string DB_CONNECTION_STRING =
                @"Data Source=EP.Balda.Data\DbStore\dictionaryDb.db";
            services.AddDbContext<WordDbContext>(
                opt =>
                {
                    opt.UseSqlite(DB_CONNECTION_STRING);
                    opt.UseQueryTrackingBehavior(
                        QueryTrackingBehavior.NoTracking);
                });
            return services;
        }

        public static IServiceCollection AddPlayerData(this IServiceCollection services)
        {
            const string DB_CONNECTION_STRING =
                @"Data Source=EP.Balda.Data\DbStore\playerDb.db";
            var assemblyName = typeof(PlayerDbContext).Namespace;

            services.AddDbContext<PlayerDbContext>(
                opt =>
                {
                    opt.UseSqlite(DB_CONNECTION_STRING);
                    opt.UseQueryTrackingBehavior(
                        QueryTrackingBehavior.NoTracking);
                });
            return services;
        }

        // Add-Migration InitialCreatePlayerDb -OutputDir Migrations\PlayerDbMigrations
        // -Context PlayerDbContext  -Project EP.Balda.Data  -StartupProject EP.Balda.Web
    }
}