using EP.TicTacToe.Data.Services;
using Microsoft.Extensions.DependencyInjection;

[assembly: Fody.ConfigureAwait(false)]
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