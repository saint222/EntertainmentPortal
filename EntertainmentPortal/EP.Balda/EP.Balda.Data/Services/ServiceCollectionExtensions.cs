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
                        "/EP.Balda/EP.Balda.Data/DbStore/dictionaryDb.db; " +
                        "Version=3; " +
                        "UseUTF16Encoding=True;");
                    opt.UseQueryTrackingBehavior(
                        QueryTrackingBehavior.NoTracking);
                });
            return services;
        }
    }
}