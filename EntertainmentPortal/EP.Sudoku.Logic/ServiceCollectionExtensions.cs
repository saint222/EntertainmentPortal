using EP.Sudoku.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Sudoku.Logic
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSudokuServices(this IServiceCollection services)
        {
            services.AddSudokuData();
            return services;
        }
    }
}
