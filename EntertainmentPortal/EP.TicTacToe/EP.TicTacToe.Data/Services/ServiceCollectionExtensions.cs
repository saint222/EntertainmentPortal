using EP.TicTacToe.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EP.TicTacToe.Data.Services
{
    /// <summary>
    ///     Connection service of project database providers.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGameData(this IServiceCollection services)
        {
            services.AddDbContext<TicTacDbContext>(
                opt =>
                {
                    opt.UseSqlite(
                        @"Data Source =..\EP.TicTacToe.Data\DbStore\tictactoeDb.db");
                    opt.UseQueryTrackingBehavior(
                        QueryTrackingBehavior.NoTracking);
                });
            return services;
        }
    }
}