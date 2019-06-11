using EP.Balda.Data.Models;
using EP.Balda.Data.Models.ModelBuilder;
using Microsoft.EntityFrameworkCore;

namespace EP.Balda.Data.Context
{
    /// <summary>
    ///     player database context
    /// </summary>
    public class PlayerDbContext : DbContext
    {
        public DbSet<PlayerDb> Players { get; set; }

        public PlayerDbContext(DbContextOptions<PlayerDbContext> options)
            : base(options)
        {
        }

        //call the test player generator when updating the player database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }
    }
}