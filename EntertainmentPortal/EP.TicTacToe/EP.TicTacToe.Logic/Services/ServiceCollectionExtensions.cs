using EP.TicTacToe.Data.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EP.TicTacToe.Logic.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGameServices(this IServiceCollection services)
        {
            services.AddGameData();
            return services;
        }
    }
}