using EP.TicTacToe.Data.Services;
using Fody;
using Microsoft.Extensions.DependencyInjection;

//Fodu extension
[assembly: ConfigureAwait(false)]

namespace EP.TicTacToe.Logic.Services
{
    /// <summary>
    ///     Service of forwarding mediator objects to the Dl/Pl layers.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGameServices(this IServiceCollection services)
        {
            services.AddGameData();
            return services;
        }
    }
}