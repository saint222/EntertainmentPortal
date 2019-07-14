using EP.Hangman.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EP.Hangman.Data
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGameData(this IServiceCollection services)
        {
            services.AddDbContext<GameDbContext>(opt => opt.UseSqlite("Data Source = game.db"));
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<GameDbContext>()
                .AddUserManager<UserManager<IdentityUser>>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddDefaultTokenProviders();
            return services;
        }
    }
}
