using EP.Balda.Data.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EP.Balda.Logic.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWordServices(this IServiceCollection services)
        {
            services.AddWordData();
            return services;
        }
        public static IServiceCollection AddPlayerServices(this IServiceCollection services)
        {
            services.AddPlayerData();
            return services;
        }
    }
}