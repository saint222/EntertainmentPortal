using EP.DotsBoxes.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EP.DotsBoxes.Data
{
    public static class ServiceCollectionExtensions
    {
       public static IServiceCollection CreateGameBoardData(this IServiceCollection services)
        {
           services.AddDbContext<GameBoardDbContext>(
                opt => opt.UseSqlite("Data Source=gameboard.db"));
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<GameBoardDbContext>()
                .AddUserManager<UserManager<IdentityUser>>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddDefaultTokenProviders();
            return services;
        }
    }
}
