using System;
using System.Collections.Generic;
using System.Text;
using EP.DotsBoxes.Data;
using Microsoft.Extensions.DependencyInjection;

namespace EP.DotsBoxes.Logic
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPlayerServices(this IServiceCollection services)
        {
            services.AddPlayerData();
            return services;
        }

        public static IServiceCollection CreateGameBoardServices(this IServiceCollection services)
        {
            services.CreateGameBoardData();
            return services;
        }
    }
}
