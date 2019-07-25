using System;
using System.Collections.Generic;
using System.Text;
using EP._15Puzzle.Data;
using EP._15Puzzle.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EP._15Puzzle.Logic
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDeckServices(this IServiceCollection services)
        {
            services.AddDeckData();
            return services;
        }
    }
}
