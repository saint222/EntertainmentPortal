using EP.Sudoku.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Sudoku.Logic
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPlayerServices(this IServiceCollection services)
        {
            services.AddPlayerData();
            return services;
        }
    }
}
