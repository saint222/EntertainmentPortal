using EP.Balda.Data.Models;
using EP.Balda.Data.Models.ModelBuilder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace EP.Balda.Data.Context
{
    public class PlayerDbContext : DbContext
    {
        public PlayerDbContext(DbContextOptions<PlayerDbContext> options)
            : base(options: options)
        {
        }

        public DbSet<PlayerDb> Players { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }
    }
}