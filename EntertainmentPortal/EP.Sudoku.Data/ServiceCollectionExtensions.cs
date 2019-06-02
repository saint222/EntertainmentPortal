using EP.Sudoku.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Sudoku.Data
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPlayerData(this IServiceCollection services)
        {
            services.AddDbContext<SudokuDbContext>(
                opt => opt.UseSqlite("Data Source=sudoku.db"));
            return services;
        }
    }
}
