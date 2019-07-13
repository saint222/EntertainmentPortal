using EP.Sudoku.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Sudoku.Data
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSudokuData(this IServiceCollection services)
        {
            services.AddDbContext<SudokuDbContext>(
                opt => opt.UseSqlite("Data Source=sudoku.db"));
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<SudokuDbContext>()
                .AddUserManager<UserManager<IdentityUser>>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddDefaultTokenProviders();
            return services;
        }
    }
}
