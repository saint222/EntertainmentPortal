using EP.WordsMaker.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EP.WordsMaker.Data.Context
{
    public class PlayerDbContext : DbContext
    {
        public PlayerDbContext(DbContextOptions<GameDbContext> options)
            : base(options: options)
        { }

        public DbSet<PlayerDb> Players { get; set; }
        public DbSet<GameDb> Games { get; set; }
	}
}