using EP.TicTacToe.Data.Context;
using Microsoft.AspNetCore.Identity;
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

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<TicTacDbContext>()
                .AddUserManager<UserManager<IdentityUser>>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}