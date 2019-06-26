using EP.DotsBoxes.Data;
using Microsoft.Extensions.DependencyInjection;

namespace EP.DotsBoxes.Logic
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection CreateGameBoardServices(this IServiceCollection services)
        {
            services.CreateGameBoardData();
            return services;
        }
    }
}
