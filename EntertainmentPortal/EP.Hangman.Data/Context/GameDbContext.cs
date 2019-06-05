using EP.Hangman.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EP.Hangman.Data.Context
{
    public class GameDbContext : DbContext
    {
        public GameDbContext(DbContextOptions<GameDbContext> options) 
            : base(options: options)
        {
                
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        public DbSet<GameDb> Games { get; set; }
    }
}
