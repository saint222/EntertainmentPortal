using EP.DotsBoxes.Data;
using Microsoft.Extensions.DependencyInjection;

[assembly: Fody.ConfigureAwait(false)] // to set up ConfigureAwait(false) globally in the assembly

namespace EP.DotsBoxes.Logic
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPlayerServices(this IServiceCollection services)
        {
            services.AddPlayerData();
            return services;
        }

        public static IServiceCollection CreateGameBoardServices(this IServiceCollection services)
        {
            services.CreateGameBoardData();
            return services;
        }
    }
}
