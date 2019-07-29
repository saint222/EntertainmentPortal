using EP.Sudoku.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EP.Sudoku.Data
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSudokuData(this IServiceCollection services)
        {
            services.AddDbContext<SudokuDbContext>(
                opt => opt.UseSqlite(@"Data Source=..\EP.Sudoku.Data\sudoku.db"));
            return services;
        }
    }
}
