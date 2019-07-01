using EP.TicTacToe.Data;
using Microsoft.Extensions.DependencyInjection;

namespace EP.TicTacToe.Logic
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
