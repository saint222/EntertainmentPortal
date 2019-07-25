using Microsoft.Extensions.DependencyInjection;
using EP.SeaBattle.Data;


namespace EP.SeaBattle.Logic
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSeaBattleServices(this IServiceCollection services)
        {
            services.AddSeaBattleData();
            return services;
        }
    }
}
