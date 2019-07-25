using System;
using System.Collections.Generic;
using System.Text;
using EP._15Puzzle.Data.Context;
using EP._15Puzzle.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EP._15Puzzle.Data
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDeckData(this IServiceCollection services)
        {
            services.AddDbContext<DeckDbContext>(
                opt => opt.UseSqlite("Data Source=database.db"));
            services.AddIdentity<UserDb, IdentityRole>()
                .AddEntityFrameworkStores<DeckDbContext>()
                .AddUserManager<UserManager<UserDb>>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
