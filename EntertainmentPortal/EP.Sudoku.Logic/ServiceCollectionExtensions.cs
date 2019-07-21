using EP.Sudoku.Data;
using Microsoft.Extensions.DependencyInjection;

[assembly: Fody.ConfigureAwait(false)] // to set up ConfigureAwait(false) globally in the assembly
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
