using EP.WordsMaker.Data.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace EP.WordsMaker.Logic.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPlayerServices(this IServiceCollection services)
        {
            services.AddPlayerData();
            return services;
        }
    }
}