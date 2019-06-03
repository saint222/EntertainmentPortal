using EP.Hangman.Data;
using Microsoft.Extensions.DependencyInjection;

namespace EP.Hangman.Logic
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
