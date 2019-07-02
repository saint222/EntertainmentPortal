using EP.Balda.Data.Context;
using EP.Balda.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EP.Balda.Data.Services
{
    /// <summary>
    /// Connection service of project database providers.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
       
        public static IServiceCollection AddBaldaGameData(this IServiceCollection services)
        {
            services.AddDbContext<BaldaGameDbContext>(
                opt =>
                {
                    opt.UseSqlite(@"Data Source =..\EP.Balda.Data\DbStore\baldaGameDb.db");
                });

            services.AddIdentity<PlayerDb, IdentityRole>(
                opt =>
                {
                    opt.Password.RequiredUniqueChars = 0;
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequireDigit = false;
                    opt.Password.RequireNonAlphanumeric = false;
                }
                )
                .AddEntityFrameworkStores<BaldaGameDbContext>()
                .AddUserManager<UserManager<PlayerDb>>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}