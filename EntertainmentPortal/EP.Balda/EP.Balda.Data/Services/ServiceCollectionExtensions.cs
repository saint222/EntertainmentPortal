using EP.Balda.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EP.Balda.Data.Services
{
    /// <summary>
    /// Connection service of project database providers.
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
    }
}