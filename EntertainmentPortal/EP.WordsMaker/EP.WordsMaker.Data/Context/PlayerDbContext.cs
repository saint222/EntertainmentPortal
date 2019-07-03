using EP.WordsMaker.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EP.WordsMaker.Data.Context
{
    public class PlayerDbContext : IdentityDbContext
    {
        public PlayerDbContext(DbContextOptions<GameDbContext> options)
            : base(options: options)
        { }

        public DbSet<PlayerDb> Players { get; set; }
        public DbSet<GameDb> Games { get; set; }
	}
}