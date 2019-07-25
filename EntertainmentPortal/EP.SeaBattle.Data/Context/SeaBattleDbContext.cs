using EP.SeaBattle.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EP.SeaBattle.Data.Context
{
    public class SeaBattleDbContext : IdentityDbContext
    {
        public SeaBattleDbContext(DbContextOptions<SeaBattleDbContext> options)
            : base(options: options)
        {
        }

        public DbSet<PlayerDb> Players { get; set; }

        public DbSet<GameDb> Games { get; set; }

        public DbSet<ShipDb> Ships { get; set; }

        public DbSet<CellDb> Cells { get; set; }
        public DbSet<ShotDb> Shots { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PlayerDb>()
                        .HasMany(p => p.Ships)
                        .WithOne(s => s.Player)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PlayerDb>()
                        .HasMany(p => p.Shots)
                        .WithOne(s => s.Player)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ShipDb>()
                        .HasMany(s => s.Cells)
                        .WithOne(c => c.Ship)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GameDb>()
                        .HasMany(g => g.Players)
                        .WithOne(p => p.Game)
                        .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
