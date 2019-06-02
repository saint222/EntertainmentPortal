using EP.WordsMaker.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EP.WordsMaker.Data.Context
{
    public class PlayerDbContext : DbContext
    {
        public PlayerDbContext(DbContextOptions<PlayerDbContext> options)
            : base(options: options)
        { }

        public DbSet<PlayerDb> Players { get; set; }
    }
}