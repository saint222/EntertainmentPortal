using System;
using EP.Balda.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EP.Balda.Data.Services
{
    public static class ServiceCollectionExtensions
    {
        private const string STORE_PATH = @"Data Source=..\EP.Balda.Data\DbStore\";

        public static IServiceCollection AddWordData(this IServiceCollection services)
        {
            const string CONNECTION_TO_DICTIONARY_DB =
                STORE_PATH + "dictionaryDb.db";

            services.AddDbContext<WordDbContext>(
                opt =>
                {
                    opt.UseSqlite(CONNECTION_TO_DICTIONARY_DB);
                    opt.UseQueryTrackingBehavior(
                        QueryTrackingBehavior.NoTracking);
                });
            return services;
        }

        public static IServiceCollection AddPlayerData(this IServiceCollection services)
        {
            const string CONNECTION_TO_PLAYER_DB =
                STORE_PATH + "playerDb.db";

            //string startupPath = Environment.CurrentDirectory;
            Console.WriteLine(CONNECTION_TO_PLAYER_DB);

            services.AddDbContext<PlayerDbContext>(
                opt =>
                {
                    opt.UseSqlite(CONNECTION_TO_PLAYER_DB);
                    opt.UseQueryTrackingBehavior(
                        QueryTrackingBehavior.NoTracking);
                });
            return services;
        }

        // Add-Migration InitialCreatePlayerDb -OutputDir Migrations\PlayerDbMigrations
        // -Context PlayerDbContext -Project EP.Balda.Data -StartupProject EP.Balda.Web

        //Update-Database InitialCreatePlayerDb -Context PlayerDbContext
        //-Project EP.Balda.Data -StartupProject EP.Balda.Web
    }
}