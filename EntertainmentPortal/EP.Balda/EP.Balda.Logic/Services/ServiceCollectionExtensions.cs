using EP.Balda.Data.Services;
using Fody;
using Microsoft.Extensions.DependencyInjection;

[assembly: ConfigureAwait(false)]

namespace EP.Balda.Logic.Services
{
    /// <summary>
    ///     Service of forwarding mediator objects to the dl/pl layers.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBaldaGameServices(
            this IServiceCollection services)
        {
            services.AddBaldaGameData();
            return services;
        }
    }
}