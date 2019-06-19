using EP.Balda.Data.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EP.Balda.Logic.Services
{
    /// <summary>
    ///     Service of forwarding mediator objects to the dl/pl layers.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBaldaGameServices(this IServiceCollection services)
        {
            services.AddBaldaGameData();
            return services;
        }
    }
}