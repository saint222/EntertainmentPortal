using EP.Balda.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EP.Balda.Data.Services
{
    /// <summary>
    ///     Connection service of project database providers.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
       
        public static IServiceCollection AddBaldaGameData(this IServiceCollection services)
        {
            services.AddDbContext<BaldaGameDbContext>(
                opt =>
                {
                    opt.UseSqlite(@"Data Source =..\EP.Balda.Data\DbStore\baldaGameDb.db");
                });
            return services;
        }

        // Add-Migration InitialCreatePlayerDb -OutputDir Migrations\PlayerDbMigrations
        // -Context PlayerDbContext -Project EP.Balda.Data -StartupProject EP.Balda.Web

        //Update-Database InitialCreatePlayerDb -Context PlayerDbContext
        //-Project EP.Balda.Data -StartupProject EP.Balda.Web


        //Add-Migration AddInitialPlayerDb -OutputDir Migrations\PlayerDbMigrations
        //-Context PlayerDbContext -Project EP.Balda.Data -StartupProject EP.Balda.Web

        //Update-Database AddInitialPlayerDb -Context PlayerDbContext
        //-Project EP.Balda.Data -StartupProject EP.Balda.Web

        //Remove-Migration -Context PlayerDbContext -Project EP.Balda.Data -StartupProject EP.Balda.Web
    }
}