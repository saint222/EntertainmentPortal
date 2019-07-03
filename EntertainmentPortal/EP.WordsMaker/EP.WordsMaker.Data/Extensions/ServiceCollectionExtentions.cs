using EP.WordsMaker.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EP.WordsMaker.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPlayerData(this IServiceCollection services)
        {
            services.AddDbContext<GameDbContext>(opt => opt.UseSqlite("Data Source=wordsMaker.db"));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<GameDbContext>()
                .AddUserManager<UserManager<IdentityUser>>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddDefaultTokenProviders();
            //services.AddDbContext<GameDbContext>(opt => opt.UseSqlite("Data Source=player.db"));
            return services;
        }
    }
}